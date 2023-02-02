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
    public class CorreiosPage : Helper
    {
        public string LCorreios = "https://buscacepinter.correios.com.br/app/endereco/index.php";
        private string IdFieldCEP = "endereco";
        private string IdbtnPesquisa = "btn_pesquisar";
        private string XPathTabelaRua = "//table[@id='resultado-DNEC']//td[@data-th='Logradouro/Nome']";
        private string XPathTabelaCidade = "//table[@id='resultado-DNEC']//td[@data-th='Localidade/UF']";
        private string XPathTabelaCEP = "//table[@id='resultado-DNEC']//td[@data-th='CEP']";
        private string XPathResultTitle = "//h5[text()='Resultado da Busca por Endereço ou CEP']";
        private string XPathLinkCorreios = "//a[@class='logo']";
        private string IdInputRastreamento = "objetos";
        private string ClassNameIconSearchRastreamento = "ic-busca-out";
        private string IdCaptchImg = "//div[@class='controle']/img";
        public string PathToSaveImg = @"C:\temp\";

        public string IdNewBusca = "btn_nbusca";

        public CorreiosPage() : base(null) { }

        public void FillFieldCEP(string CEP)
        {
            this.SendKeys(By.Id(IdFieldCEP), CEP);
        }

        public void FillFieldCodRastreamento(string cod)
        {
            this.WaitUntilElement(By.XPath(IdInputRastreamento));
            this.SendKeys(By.Id(IdInputRastreamento), cod);
        }
        public string GetTextCaptcha()
        {
            var pathImg = this.SaveImage(IdCaptchImg, PathToSaveImg);
            return this.GetTextByImg(pathImg);
        }
        public void ClickSearchRastreamento()
        {
            this.Click(By.ClassName(ClassNameIconSearchRastreamento));
        }

        public string GetTableResultSearch(string alias)
        {
            switch (alias.ToLower())
            {
                case "street":
                    return this.GetText(By.XPath(XPathTabelaRua));
                case "uf":
                    return this.GetText(By.XPath(XPathTabelaCidade));
                case "cep":
                    return this.GetText(By.XPath(XPathTabelaCEP));
                default:
                    throw new Exception("Opção inválida.");
            }
            
        }
        public bool WaitUntilTitleResultIsPresent()
        {
            return this.WaitUntilElement(By.XPath(XPathResultTitle));
        }
        public void ClickNewBusca()
        {
            this.Click(By.Id(IdNewBusca));
        }
        public void NavigateToSiteCEP()
        {
            try
            {
                this.NavigateToURL(LCorreios);
            }
            catch (Exception)
            {

                throw new Exception("Não foi possível navegar na url");
            }
        }
        public void ClickBuscarCEP()
        {
            this.Click(By.Id(IdbtnPesquisa));
        }
        public bool TextResultRastreamento(string text)
        {
            try
            {
                string xpath = "//h6[text()='" + text + "']";
                this.WaitUntilElement(By.XPath(xpath));
                this.GetText(By.XPath(xpath));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void ClickImgCorreios()
        {
            this.Click(By.XPath(XPathLinkCorreios));
        }
    }
}
