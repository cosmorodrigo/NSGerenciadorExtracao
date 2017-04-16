using System.Configuration;

namespace NSGerenciadorExtracao.Configuracao
{
    public class Configuracao
    {
        public Configuracao()
        {
            this.CaminhoServidor = ConfigurationManager.AppSettings["CaminhoServidor"];
            this.CaminhoUploadTemporario = ConfigurationManager.AppSettings["CaminhoUploadTemporario"];
            this.CaminhoUploadArquivo = ConfigurationManager.AppSettings["CaminhoUploadArquivo"];
        }

        public string CaminhoServidor { get; private set; }
        public string CaminhoUploadTemporario { get; private set; }
        public string CaminhoUploadArquivo { get; private set; }
    }
}
