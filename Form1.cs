using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public List<StudentInfo> studentlist = new List<StudentInfo>();//信息列表
        public XmlDocument doc = new XmlDocument();//xml读取
        public String xmladdr;//xml相对地址
        public Form1()
        {
            InitializeComponent();
            xmladdr= "../../Students.xml";

            doc.Load(xmladdr);
        }
       
       

        public void setView()
        {
           this.ShowView.Rows.Clear();
            studentlist = new List<StudentInfo>();
            XmlElement rootElem = doc.DocumentElement;   //获取根节点  
            XmlNodeList xnl = rootElem.GetElementsByTagName("记录");
            foreach (XmlNode xn1 in xnl)//遍历节点数据 存储
            {
                StudentInfo st = new StudentInfo();
                XmlElement element = (XmlElement)xn1;
                XmlNodeList li = element.ChildNodes;
                st.ID = Convert.ToInt64(li.Item(0).InnerText);
                st.name = li.Item(1).InnerText;
                st.age = Convert.ToInt16(li.Item(2).InnerText);
                st.major = li.Item(3).InnerText;
                studentlist.Add(st);
            }
            int index;
            for (int i = 0; i < studentlist.Count; i++)//在列表中展示数据
            {
                index = this.ShowView.Rows.Add();
                this.ShowView.Rows[index].Cells[0].Value = studentlist[i].ID;
                this.ShowView.Rows[index].Cells[1].Value = studentlist[i].name;
                this.ShowView.Rows[index].Cells[2].Value = studentlist[i].age;
                this.ShowView.Rows[index].Cells[3].Value = studentlist[i].major;
                this.ShowView.Rows[index].Cells[4].Value = "修改";
                this.ShowView.Rows[index].Cells[5].Value = "删除";
            }
        }
      
        private void button2_Click(object sender, EventArgs e)//增加一条数据
        {
           
            doc.Load(xmladdr);
            XmlElement rootElem = doc.DocumentElement;   //获取根节点  
            String idvalue = textBox1.Text;
            if(!System.Text.RegularExpressions.Regex.IsMatch(idvalue, @"^\d*$")||idvalue=="")
            {
                MessageBox.Show("学号请输入数字！", "提示");
                return;
            }
            String agevalue = textBox3.Text;
            if (!System.Text.RegularExpressions.Regex.IsMatch(agevalue, @"^\d*$"))
            {
                MessageBox.Show("年龄请输入介于0~120的数字！", "提示");
                return;
            }
           String namevalue = textBox2.Text;
            if (namevalue == "")
            {
                MessageBox.Show("姓名不得为空！", "提示");
                return;
            }
            String majorvalue = textBox4.Text;
            if (majorvalue == "")
            {
                MessageBox.Show("专业不得为空！", "提示");
                return;
            }

            XmlElement add = doc.CreateElement("记录");
            XmlElement id = doc.CreateElement("学号");
            id.InnerText = idvalue;
            XmlElement name = doc.CreateElement("姓名");
            name.InnerText = namevalue;
            XmlElement age = doc.CreateElement("年龄");
            age.InnerText = agevalue; ;
            XmlElement major = doc.CreateElement("专业");
            major.InnerText =majorvalue;
            add.AppendChild(id);
            add.AppendChild(name);
            add.AppendChild(age);
            add.AppendChild(major);
            rootElem.AppendChild(add);
            doc.Save(xmladdr);
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            this.setView();
        }

        private void ShowView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ShowView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == ShowView.Columns[5].Index && ShowView.Rows[e.RowIndex].Cells[5].Value.ToString() == "删除")
            {
                String id = ShowView.Rows[e.RowIndex].Cells[0].Value.ToString();
                String name= ShowView.Rows[e.RowIndex].Cells[1].Value.ToString();
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要删除" + id + "的信息吗?", "确定", messButton);
                if (dr == DialogResult.OK)//如果点击“确定”按钮
                {
                   
                    XmlElement rootElem = doc.DocumentElement;   //获取根节点  
                    XmlNodeList xnl = rootElem.GetElementsByTagName("记录");
                    foreach (XmlNode xn1 in xnl)
                    {
                        XmlElement element = (XmlElement)xn1;
                        XmlNodeList li = element.ChildNodes;
                        if (li.Item(0).InnerText==id&&li.Item(1).InnerText==name)
                        {
                            xn1.ParentNode.RemoveChild(xn1);
                            doc.Save(xmladdr);
                            this.setView();
                            break;
                        }
                    }
                }
                else//如果点击“取消”按钮
                {

                }
            }

            if (e.ColumnIndex == ShowView.Columns[4].Index && ShowView.Rows[e.RowIndex].Cells[4].Value.ToString() == "修改")
            {
                String id = ShowView.Rows[e.RowIndex].Cells[0].Value.ToString();
                String name = ShowView.Rows[e.RowIndex].Cells[1].Value.ToString();
                String age = ShowView.Rows[e.RowIndex].Cells[2].Value.ToString();
                String major = ShowView.Rows[e.RowIndex].Cells[3].Value.ToString();

                if (!System.Text.RegularExpressions.Regex.IsMatch(id, @"^\d*$") || id == "")
                {
                    MessageBox.Show("学号请输入数字！", "提示");
                    return;
                }
                if (!System.Text.RegularExpressions.Regex.IsMatch(age, @"^\d*$"))
                {
                    MessageBox.Show("年龄请输入介于0~120的数字！", "提示");
                    return;
                }
             
 XmlElement rootElem = doc.DocumentElement;   //获取根节点  
                    XmlNodeList xnl = rootElem.GetElementsByTagName("记录");
                    foreach (XmlNode xn1 in xnl)
                    {
                        XmlElement element = (XmlElement)xn1;
                        XmlNodeList li = element.ChildNodes;
                    if (li.Item(0).InnerText ==id||li.Item(1).InnerText == name||li.Item(2).InnerText==age||li.Item(0).InnerText==major)
                    {
                        foreach (XmlNode temp in li)
                        {
                            if (temp.Name == "学号")
                            {
                                temp.InnerText = id;
                            }
                            if (temp.Name == "姓名")
                            {
                                temp.InnerText = name;
                            }
                            if (temp.Name == "年龄")
                            {
                                temp.InnerText = age;
                            }
                            if (temp.Name == "专业")
                            {
                                temp.InnerText = major;
                            }
                        }
                        doc.Save(xmladdr);
                        this.setView();
                        break;
                    }
                    }
                }
               



            }
        private void button1_Click(object sender, EventArgs e)
        {
            this.setView();
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
          
        }
  
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
