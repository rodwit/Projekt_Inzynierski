using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;

namespace FunctionalTestsProject
{
	[TestClass]
	public class StandUpTests
	{
		public IWebDriver Driver { get; set; }

		[TestMethod]
		public void SmokeTest()
		{
			Driver = new FirefoxDriver(".");
			Driver.Navigate().GoToUrl("https://localhost:44325/Default.aspx");
		}

		[TestMethod]
		public void LoginTest()
		{
			SmokeTest();
			var loginTab = Driver.FindElement(By.LinkText("Zaloguj"));
			loginTab.Click();
			var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
			wait.Until(e => e.FindElement(By.Id("module_name")).Text.Contains("Logowanie"));
			string login = "t@t";
			string pass = "t";
			var loginInput = Driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxAddres"));
			var loginPass = Driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxPassword"));
			var buttonloginInput = Driver.FindElement(By.Id("ContentPlaceHolder1_ButtonLogIn"));
			loginInput.SendKeys(login);
			loginPass.SendKeys(pass);
			buttonloginInput.Click();
			wait.Until(e => e.FindElement(By.Id("module_name")).Text.Contains("Strona Główna"));
		}

		[TestMethod]
		public void ProductViewTest()
		{
			LoginTest();
			var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
			var productsTab = Driver.FindElement(By.LinkText("Produkty"));
			productsTab.Click();
			wait.Until(e => e.FindElement(By.Id("module_name")).Text.Contains("Produkty"));
		}		

		[TestMethod]
		public void ContractorViewTest()
		{
			LoginTest();
			var contractorTab = Driver.FindElement(By.LinkText("Kontrahenci"));
			contractorTab.Click();
			var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
			wait.Until(e => e.FindElement(By.Id("module_name")).Text.Contains("Kontrahenci"));
		}

		[TestMethod]
		public void InvoiceViewTest()
		{
			LoginTest();
			var invoiceTab = Driver.FindElement(By.LinkText("Faktury"));
			invoiceTab.Click();
			var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
			wait.Until(e => e.FindElement(By.Id("module_name")).Text.Contains("Faktury"));
		}

		[TestMethod]
		public void ProductAddViewTest()
		{
			LoginTest();
			var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
			var productsTab = Driver.FindElement(By.LinkText("Produkty"));
			productsTab.Click();
			wait.Until(e => e.FindElement(By.Id("module_name")).Text.Contains("Produkty"));
			var buttonAddProduct = Driver.FindElement(By.LinkText("Dodaj produkt"));
			buttonAddProduct.Click();
			wait.Until(e => e.FindElement(By.Id("module_name")).Text.Contains("Dodaj produkt"));
		}


		[TestMethod]
		public void ProductAddTest()
		{
			ProductViewTest();
			var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
			var buttonAddProduct = Driver.FindElement(By.LinkText("Dodaj produkt"));
			buttonAddProduct.Click();
			wait.Until(e => e.FindElement(By.Id("module_name")).Text.Contains("Dodaj produkt"));
			var productName = "TestowyProdukt";
			var productNameInput = Driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxName"));
			var buttonAdd = Driver.FindElement(By.Id("ContentPlaceHolder1_ButtonAddProduct"));
			productNameInput.SendKeys(productName);
			buttonAdd.Click();
			wait.Until(e => e.FindElement(By.Id("module_name")).Text.Contains("Produkty"));
			Driver.FindElement(By.Id("ContentPlaceHolder1_GridViewProducts")).Text.Equals(productName);
		}

		[TestMethod]
		public void ContractorAddViewTest()
		{
			ContractorViewTest();
			var contractorTab = Driver.FindElement(By.LinkText("Kontrahenci"));
			contractorTab.Click();
			var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
			wait.Until(e => e.FindElement(By.Id("module_name")).Text.Contains("Kontrahenci"));

		}

		[TestMethod]
		public void AddContractorTest()
		{
			ContractorAddViewTest();
			var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
			wait.Until(e => e.FindElement(By.Id("module_name")).Text.Contains("Kontrahenci"));
			var contractorAddButton = Driver.FindElement(By.LinkText("Dodaj kontrahenta"));
			contractorAddButton.Click();
			wait.Until(e => e.FindElement(By.Id("module_name")).Text.Contains("Dodaj kontrahenta"));
			var textBoxNameInput = Driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxName"));
			textBoxNameInput.SendKeys("Test");
			var textBoxStreetInput = Driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxStreet"));
			textBoxStreetInput.SendKeys("Test");
			var textBoxCityInput = Driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxCity"));
			textBoxCityInput.SendKeys("Cracow");
			var textBoxPostCodeInput = Driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxPostCode"));
			textBoxPostCodeInput.SendKeys("11-111");
			var textBoxPostTownInput = Driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxPostTown"));
			textBoxPostTownInput.SendKeys("Cracow");
			var textBoxNipInput = Driver.FindElement(By.Id("ContentPlaceHolder1_TextBoxNIP"));
			textBoxNipInput.SendKeys("9451972201");
			textBoxPostTownInput.Click();
			var saveButton = Driver.FindElement(By.Id("ContentPlaceHolder1_ButtonSave"));
			saveButton.Click();
			wait.Until(e => e.FindElement(By.Id("module_name")).Text.Contains("Kontrahenci"));
			Driver.FindElement(By.Id("ContentPlaceHolder1_GridViewContractors")).Text.Equals("Test");			
		}

		[TestMethod]
		public void QuitTest()
		{
			LoginTest();
			var logoutTab = Driver.FindElement(By.LinkText("Wyloguj"));
			logoutTab.Click();
			var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
			wait.Until(e => e.FindElement(By.Id("module_name")).Text.Contains("Wylogowywanie..."));
			wait.Until(e => e.FindElement(By.Id("module_name")).Text.Contains("Strona Główna"));
		}
	}
}
