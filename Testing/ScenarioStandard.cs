﻿//******************************************************************************
//
// Copyright (c) 2017 Microsoft Corporation. All rights reserved.
//
// This code is licensed under the MIT License (MIT).
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//******************************************************************************

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing
{
    [TestClass]
    public class ScenarioStandard : GKSession
    {

        [TestMethod]
        public void Register()
        {
            var splitViewPane = session.FindElementByClassName("SplitViewPane");
            splitViewPane.FindElementByAccessibilityId("MenuButton1").Click();

            session.FindElementByName("Vorname:").Click();
            session.FindElementByName("Vorname:").SendKeys("Marvin");
            session.FindElementByName("Nachname:").Click();
            session.FindElementByName("Nachname:").SendKeys("Ertl");
            session.FindElementByName("E-Mail:").Click();
            session.FindElementByName("E-Mail:").SendKeys("abc@def.com");
            session.FindElementByName("Passwort:").Click();
            session.FindElementByName("Passwort:").SendKeys("12345");
            session.FindElementByName("Passwort wiederholen:").Click();
            session.FindElementByName("Passwort wiederholen:").SendKeys("12345");
            session.FindElementByXPath("//Button[@Name='Registrieren']").Click();

            Assert.AreEqual("Herzlich Willkommen", session.FindElementByName("Herzlich Willkommen").Text);
        }

        [TestMethod]
        public void Logout()
        {
            var splitViewPane = session.FindElementByClassName("SplitViewPane");
            splitViewPane.FindElementByAccessibilityId("MenuButton1").Click();

            Assert.AreEqual("Login", session.FindElementByName("Login").Text);
        }

        [TestMethod]
        public void Login()
        {
            var splitViewPane = session.FindElementByClassName("SplitViewPane");
            splitViewPane.FindElementByAccessibilityId("MenuButton2").Click();

            session.FindElementByName("E-Mail:").Click();
            session.FindElementByName("E-Mail:").SendKeys("abc@def.com");
            session.FindElementByName("Passwort:").Click();
            session.FindElementByName("Passwort:").SendKeys("12345");
            session.FindElementByXPath("//Button[@Name='Login']").Click();

            Assert.AreEqual("Herzlich Willkommen", session.FindElementByName("Herzlich Willkommen").Text);
        }

        [TestMethod]
        public void DeleteUser()
        {
            var splitViewPane = session.FindElementByClassName("SplitViewPane");
            splitViewPane.FindElementByAccessibilityId("MenuButton2").Click();

            Assert.AreEqual("Login", session.FindElementByName("Login").Text);
        }

        [TestMethod]
        public void RegisterExistingUser()
        {

            Register();
            var splitViewPane = session.FindElementByClassName("SplitViewPane");
            splitViewPane.FindElementByAccessibilityId("MenuButton1").Click();
            splitViewPane = session.FindElementByClassName("SplitViewPane");
            splitViewPane.FindElementByAccessibilityId("MenuButton1").Click();
            session.FindElementByName("Vorname:").Click();
            session.FindElementByName("Vorname:").SendKeys("Marvin");
            session.FindElementByName("Nachname:").Click();
            session.FindElementByName("Nachname:").SendKeys("Ertl");
            session.FindElementByName("E-Mail:").Click();
            session.FindElementByName("E-Mail:").SendKeys("abc@def.com");
            session.FindElementByName("Passwort:").Click();
            session.FindElementByName("Passwort:").SendKeys("12345");
            session.FindElementByName("Passwort wiederholen:").Click();
            session.FindElementByName("Passwort wiederholen:").SendKeys("12345");
            session.FindElementByXPath("//Button[@Name='Registrieren']").Click();

            Assert.AreEqual("E-Mail-Adresse bereits im System vorhanden.", session.FindElementByName("E-Mail-Adresse bereits im System vorhanden.").Text);
        }

        [TestMethod]
        public void LoginNotExsistingUser()
        {
            var splitViewPane = session.FindElementByClassName("SplitViewPane");
            splitViewPane.FindElementByAccessibilityId("MenuButton2").Click();

            session.FindElementByName("E-Mail:").Click();
            session.FindElementByName("E-Mail:").SendKeys("abc1@def.com");
            session.FindElementByName("Passwort:").Click();
            session.FindElementByName("Passwort:").SendKeys("12345");
            session.FindElementByXPath("//Button[@Name='Login']").Click();
            
            Assert.AreEqual("E-Mail-Adresse im System nicht vorhanden.", session.FindElementByName("E-Mail-Adresse im System nicht vorhanden.").Text);
        }

        [TestMethod]
        public void LoginWrongPassword()
        {
            var splitViewPane = session.FindElementByClassName("SplitViewPane");
            splitViewPane.FindElementByAccessibilityId("MenuButton1").Click();
            splitViewPane = session.FindElementByClassName("SplitViewPane");
            splitViewPane.FindElementByAccessibilityId("MenuButton2").Click();

            session.FindElementByName("E-Mail:").Click();
            session.FindElementByName("E-Mail:").SendKeys("abc@def.com");
            session.FindElementByName("Passwort:").Click();
            session.FindElementByName("Passwort:").SendKeys("123456");
            session.FindElementByXPath("//Button[@Name='Login']").Click();

            Assert.AreEqual("Passwort ist falsch eingegeben worden.", session.FindElementByName("Passwort ist falsch eingegeben worden.").Text);
        }

        [TestMethod]
        public void LoginAndDelete()
        {
            var splitViewPane = session.FindElementByClassName("SplitViewPane");
            splitViewPane.FindElementByAccessibilityId("MenuButton1").Click();
            splitViewPane = session.FindElementByClassName("SplitViewPane");
            splitViewPane.FindElementByAccessibilityId("MenuButton2").Click();
            Login();

            splitViewPane = session.FindElementByClassName("SplitViewPane");
            splitViewPane.FindElementByAccessibilityId("MenuButton2").Click();

            Assert.AreEqual("Login", session.FindElementByName("Login").Text);
        }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            // Create session to launch a window
            Setup(context);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            TearDown();
        }
    }
}
