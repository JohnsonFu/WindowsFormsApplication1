using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
   public class StudentInfo
    {
        public long ID;
        public String name;
        public int age;
        public String major;
        public StudentInfo()
        {

        }

     public StudentInfo(long id,String name,int age,String major)
        {
            this.ID = id;
            this.name = name;
            this.age = age;
            this.major = major;
        }
          public long getID()
        {
            return this.ID;
        }
        public void setID(long id)
        {
            this.ID = id;
        }
        public String getName()
        {
            return this.name;
        }
        public void setName(String name)
        {
            this.name = name;
        }

        public long getAge()
        {
            return this.age;
        }
        public void setAge(int age)
        {
            this.age = age;
        }
        public String getMajor()
        {
            return this.major;
        }
        public void setMajor(String major)
        {
            this.major = major;
        }

    }
}
