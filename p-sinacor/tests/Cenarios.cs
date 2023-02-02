using System;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using p_sinacor.Pages;

namespace SeleniumTests
{
    [TestClass]
    public class Cenarios
    {
        private static Pages pages;
        private StringBuilder verificationErrors;

        [ClassInitialize]
        public static void InitializeClass(TestContext testContext)
        {
            pages = new Pages();
        }

        [ClassCleanup]
        public static void CleanupClass()
        {
            try
            {
                pages.correioPage.Close(); 
                pages.linkCorreiosPage.Close();               
            }
            catch (Exception)
            {
            }
        }


        [TestInitialize]
        public void InitializeTest()
        {
            verificationErrors = new StringBuilder();
        }

        [TestCleanup]
        public void CleanupTest()
        {
            Assert.AreEqual("", verificationErrors.ToString());
            pages.correioPage.Quit();
            pages.linkCorreiosPage.Quit();
        }

        [TestMethod]
        public void TestValidationCEP()
        {
            #region Primeiro CEP
            pages.correioPage.NavigateToSiteCEP();
            pages.correioPage.FillFieldCEP("80700000");
            pages.correioPage.ClickBuscarCEP();
            #endregion

            #region Validação CEP 1
            Assert.IsTrue(pages.correioPage.TextResultRastreamento("Dados não encontrado"));
            #endregion

            #region Segundo CEP
            pages.correioPage.ClickNewBusca();
            pages.correioPage.FillFieldCEP("01013-001");
            pages.correioPage.ClickBuscarCEP();
            #endregion

            #region Espera implicita
            pages.correioPage.WaitUntilTitleResultIsPresent();           
            #endregion

            #region Validação CEP 2
            Assert.AreEqual(pages.correioPage.GetTableResultSearch("street"), "Rua Quinze de Novembro - lado ímpar");
            Assert.AreEqual(pages.correioPage.GetTableResultSearch("uf"), "São Paulo/SP");
            #endregion   
        }
        [TestMethod]
        public void TestValidationCodRastreamento()
        {
            // Como é necessário digitar em um campo números e letrar de uma imagem(captcha)
            // não foi possível realizar o rastreamento,
            // Em uma tentativa de extrair p texto da iamgem, foi utilizado o Tesseract, mas sem sucesso.
            // Como alternativa, o rastreamento será feito através do site https://www.linkcorreios.com.br/
            pages.linkCorreiosPage.NavigateToSiteLinkCorreios();
            pages.linkCorreiosPage.FillFieldCodRastreamento("SS987654321BR");
            pages.linkCorreiosPage.ClickSearchRastreamento();
            pages.linkCorreiosPage.WaitUntilResultRastreamento();

            bool rastreamentoIndisponivel = pages.linkCorreiosPage.TextExistInPage("O rastreamento não está disponível no momento");
            Assert.IsTrue(rastreamentoIndisponivel);
        }
    }
}
