using GusLibrary;
using MfLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projekt_Inzynierski;
using System;
using System.Collections.Generic;

namespace Projekt_InzynierskiTests
{
	[TestClass]
	public class UnitTests
	{
		[TestMethod]
		public void BaseConnection_ProperInit_ConnectionIsNotNull()
		{
			BaseConnection.openConnection();
			Assert.IsNotNull(BaseConnection.connection);
			Assert.IsNotNull(BaseConnection.connectionString);
			BaseConnection.closeConnection();
		}

		[TestMethod]
		public void GetConnection_ProperInit_ReturnConnectionObject()
		{
			BaseConnection.openConnection();
			Assert.IsNotNull(BaseConnection.GetConnection);
			BaseConnection.closeConnection();
		}

		[TestMethod]
		public void ExecProcedure_WrongName_ThrowsError()
		{
			try
			{
				BaseConnection.openConnection();
				_ = BaseConnection.execProcedure("WrongProcedure");
				BaseConnection.closeConnection();
				Assert.Fail("Should throw exception!");
			}
			catch (Exception ex)
			{
				Assert.AreEqual(
					"Could not find stored procedure 'WrongProcedure'.", 
					ex.Message
				);
			}			
		}

		[TestMethod]
		public void ExecProcedureWithParameters_CorrectName_ReturnSqlParameters()
		{
			var pair = new Dictionary<string, string>();
			pair.Add("@userId", "0");
			pair.Add("@name", "TestName");
			pair.Add("@postTown", "TestPostTown");
			pair.Add("@postCode", "TestPostCode");
			pair.Add("@city", "TestTextBoxCity");
			pair.Add("@street", "TestStreet");
			pair.Add("@nip", "TestNip");
			pair.Add("@regon", "TestRegon");
			BaseConnection.openConnection();
			var sqlParam = BaseConnection.execProcedure("AddContractor", pair);
			BaseConnection.closeConnection();
			Assert.IsNotNull(sqlParam);
		}

		[TestMethod]
		public void ExecCommand_SelectQuery_NoUpdate()
		{
			BaseConnection.openConnection();
			var result = BaseConnection.execCommand("SELECT * FROM Products;");
			BaseConnection.closeConnection();
			Assert.IsTrue(result == -1, "Number should be minus one");
		}

		[TestMethod]
		public void ExecScalar_CorrectQuery_One()
		{
			BaseConnection.openConnection();
			var result = (int)BaseConnection.execScalar("SELECT Id FROM Contractors WHERE Id = 1;");
			BaseConnection.closeConnection();
			Assert.IsTrue(result == 1, "Result should be number one");
		}
		
		[TestMethod]
		public void ExecReader_CorrectQuery_TwoValues()
		{
			BaseConnection.openConnection();
			var reader = BaseConnection.execReader("SELECT Id, Name FROM Contractors WHERE Id = 1;");
			while (reader.Read())
			{
				Assert.IsTrue(reader.GetInt32(0) == 1);
				Assert.IsTrue(reader.GetString(1) == "Mirek Inc");
			}
			BaseConnection.closeConnection();		
		}


	[TestMethod]
		public void GenerateInvoice_InitializeObject_ReturnInvoiceObject()
		{
			var idUser = 1;
			var idInvoice = 13;
			var invoiceNr = "10";
			var invoiceGenerator = new GenerateInvoice(idUser, idInvoice, invoiceNr);
			Assert.AreEqual(invoiceGenerator._idInvoice, idInvoice);
			Assert.AreEqual(invoiceGenerator._idUser, idUser);
			Assert.AreEqual(invoiceGenerator._idInvoice, idInvoice);
			Assert.IsNotNull(invoiceGenerator._idContractor);
			Assert.IsNotNull(invoiceGenerator._stringWriter);
			Assert.IsNotNull(invoiceGenerator._writer);
		}

		[TestMethod]
		public void GenerateInvoice_Header()
		{
			var idUser = 1;
			var idInvoice = 13;
			var invoiceNr = "10";
			GenerateInvoice test = new GenerateInvoice(idUser, idInvoice, invoiceNr);
			test.header();
			Assert.IsTrue(!string.IsNullOrEmpty(test._stringWriter.ToString()));
		}

		[TestMethod]
		public void GenerateInvoice_Head()
		{
			var idUser = 1;
			var idInvoice = 13;
			var invoiceNr = "10";
			GenerateInvoice test = new GenerateInvoice(idUser, idInvoice, invoiceNr);
			test.head();
			bool html = test._stringWriter.ToString().Contains("Typ dokumentu");
			Assert.IsTrue(html);
		}

		[TestMethod]
		public void GenerateInvoice_Persons()
		{
			var idUser = 1;
			var idInvoice = 13;
			var invoiceNr = "10";
			GenerateInvoice test = new GenerateInvoice(idUser, idInvoice, invoiceNr);
			test.persons();
			bool html = test._stringWriter.ToString().Contains("Kupujący");
			Assert.IsTrue(html);
		}

		[TestMethod]
		public void GenerateInvoice_Products()
		{
			var idUser = 1;
			var idInvoice = 13;
			var invoiceNr = "10";
			GenerateInvoice test = new GenerateInvoice(idUser, idInvoice, invoiceNr);
			test.products();
			bool html = test._stringWriter.ToString().Contains("Cena netto");
			Assert.IsTrue(html);
		}

		[TestMethod]
		public void GenerateInvoice_GenerateFileOnFileSystem_ThrowError()
		{
			var idUser = 1;
			var idInvoice = 13;
			var invoiceNr = "10";
			GenerateInvoice test = new GenerateInvoice(idUser, idInvoice, invoiceNr);
			string file = null;
			try
			{
				file = test.generateFile();
				Assert.Fail("Shouldn't run on file system!");
			}
			catch
			{
				Assert.IsTrue(string.IsNullOrEmpty(file));
			}			
		}

		[TestMethod]
		public void DataSearchSubjects_NotValidNipNumber_SoapEmptyObject()
		{
			var result = GusApiHelper.DataSearchSubjects("1111111111");
			Assert.IsNull(result.Miejscowosc);
			Assert.IsNull(result.Gmina);
			Assert.AreEqual(0, result.Id);
		}

		[TestMethod]
		public void SearchNip_NotValidNipNumber_RestNullObject()
		{
			var result = MfApiHelper.SearchNip("1111111111");
			Assert.IsNull(result);
		}		
	}
}
