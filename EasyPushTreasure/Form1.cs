using MyTools;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyPushTreasure
{
    public partial class Form1 : Form
    {
        public string mainUrl = "http://ytb.pc51.com/ytb/login.aspx";
        public SeleniumHelper sel = null;
        private IWebDriver iframe = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartTask();
        }
        /// <summary>
        /// 任务总入口
        /// </summary>
        public void StartTask()
        {
            EnterTheReleasePage();
            SetReleaseMessage();
        }
        /// <summary>
        /// 进入发布页面
        /// </summary>
        public void EnterTheReleasePage()
        {
            sel = new SeleniumHelper(1);
            sel.driver.Navigate().GoToUrl(mainUrl);
            MessageBoxButtons message = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("请先登录成功后，再点击确定！", "EasyPushTreasure", message);
            if (dr == DialogResult.OK)
            {
                var li = sel.FindElementsByCss(".dropdown.sub-menu")[1];
                Actions action = new Actions(sel.driver); //移动光标到指定的元素上perform
                action.MoveToElement(li).Perform();
                Thread.Sleep(1000);
                var text = sel.FindElementByLinkText("资讯信息管理");
                action.MoveToElement(text).Perform();
                Thread.Sleep(1000);
                text.Click();
                Thread.Sleep(1000);
                iframe = sel.driver.SwitchTo().Frame("mainFrame");//存在iframe,导向iframe  ProductType  fieldset 10-321-0
                var addNode = iframe.FindElement(By.Id("addNewFeedBack"));
                addNode.Click();
                Thread.Sleep(1000);
            }
        }
        /// <summary>
        /// 设置发布信息
        /// </summary>
        public void SetReleaseMessage()
        {
            var trigger = sel.FindElementByClassName("ddbox_trigger");
            trigger.Click();
            Thread.Sleep(1000);
            var common = sel.FindElementByLinkText("商务服务 / 软件开发 /");
            common.Click();
            Thread.Sleep(500);
            var title = sel.FindElementById("txtSubject");
            title.SendKeys("测试标题");
        }

    }
}
