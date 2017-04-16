using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NSGerenciadorExtracao.Configuracao;

namespace NSGerenciadorExtracao.Processador
{
    public enum TipoUpload
    {
        Temporario,
        Permanente
    }

    public class Upload
    {
        private Configuracao.Configuracao _conf = new Configuracao.Configuracao();

        public async Task<bool> UploadArquivo(string Server, byte[] Arquivo, TipoUpload TipoUpload)
        {
            string _caminho = string.Empty;            
            string _pasta = DateTime.Now.ToString("yyyyMMdd");
            string _nomeArquivo = string.Concat(DateTime.Now.ToString("yyyyMMddhhMMssfff"), "_", Guid.NewGuid().ToString().Replace("-", ""));
            switch(TipoUpload)
            {
                case TipoUpload.Temporario:

                    _caminho = _conf.CaminhoUploadTemporario;
                    
                    break;

                case TipoUpload.Permanente:

                    _caminho = _conf.CaminhoUploadArquivo;

                    break;
            }

            _caminho = Path.Combine(
                VerificaDiretorio(
                    Path.Combine(_caminho, _pasta)
                    ), 
                _nomeArquivo + ".csv");

            using (FileStream _destino = new FileStream(_caminho, FileMode.Create, FileAccess.Write))
            {
                await _destino.WriteAsync(Arquivo, 0, Arquivo.Length);                
            }

            return true;
        }

        private string VerificaDiretorio(string Caminho)
        {
            if (!Directory.Exists(Caminho))
                return Directory.CreateDirectory(Caminho).FullName.ToString();
            else
                return Caminho;
        }
    }
}
