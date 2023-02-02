using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace p_sinacor
{
    public class LinkCorreiosPage : Helper
    {
        public string LinkCorreios = "https://www.linkcorreios.com.br/";
        private string IdInputRastreamento = "id";
        public string XPathBtnEnviar = "//input[@value='Enviar']";

        public string XPathResultRastreamento = "//span[contains(text(),'Rastreamento Correios de Objeto')]";

        public LinkCorreiosPage() : base(null) { }

        public void FillFieldCodRastreamento(string cod)
        {
            this.WaitUntilElement(By.XPath(IdInputRastreamento));
            this.SendKeys(By.Id(IdInputRastreamento), cod);
        }
        public void ClickSearchRastreamento()
        {
            this.Click(By.XPath(XPathBtnEnviar));
        }
        public void WaitUntilResultRastreamento()
        {
            bool found = this.WaitUntilElement(By.XPath(XPathResultRastreamento));
            if (!found)  
                throw new Exception("Página do resultado do rastreamento não foi carregada.");
        }
        public bool TextExistInPage(string text)
        {
            try
            {               
                string xpath = "//p['" + text + "']";
                string result = this.GetText(By.XPath(xpath));
                return result.Contains(text);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void NavigateToSiteLinkCorreios()
        {
            try
            {
                this.NavigateToURL(LinkCorreios);
            }
            catch (Exception)
            {

                throw new Exception("Não foi possível navegar na url");
            }
        }
    }
}
