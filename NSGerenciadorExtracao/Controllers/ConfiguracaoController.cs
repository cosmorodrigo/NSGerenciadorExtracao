using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using System.IO;
using NSGerenciadorExtracao.Configuracao;
using NSGerenciadorExtracao.Processador;

namespace NSGerenciadorExtracao.Controllers
{
    public class ConfiguracaoController : ApiController
    {
        NSGerenciadorExtracao.Configuracao.Configuracao _conf = 
            new NSGerenciadorExtracao.Configuracao.Configuracao();
        
        [HttpPost]        
        public async Task<HttpResponseMessage> LayoutUpload()
        {            
            HttpResponseMessage result = null;
            var request = HttpContext.Current.Request;

            if(request.Files.Count == 1)
            {
                var _arquivo = request.Files[0];

                if(_arquivo.FileName.Contains(".csv"))
                {
                    var _server = HttpContext.Current.Server.MapPath(_conf.CaminhoServidor);                    

                    Upload upload = new Upload();
                    byte[] _file = new byte[_arquivo.InputStream.Length];
                    await _arquivo.InputStream.ReadAsync(_file,0,(int)_arquivo.InputStream.Length);                                        

                    switch (await upload.UploadArquivo(_server, _file, TipoUpload.Temporario))
                    {
                        case true:

                            result = Request.CreateResponse(HttpStatusCode.OK);

                            break;

                        case false:

                            result = Request.CreateResponse(HttpStatusCode.InternalServerError);

                            break;
                    }
                }
                else
                {
                    result = Request.CreateResponse(HttpStatusCode.BadRequest, "Apenas arquivos do tipo csv são aceitos");
                }                
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest, "Selecione apenas 1 arquivo por vez");
            }

            return result;
        }
    }
}
