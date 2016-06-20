using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Join
{
    class MemberVO
    {
        private string id;
        private string pw;
        private string name;
        private string phoneNumber;
        private string postalCode;
        private string address;
        private string email;
        private string sex;
        private string brithDay;
 
        public MemberVO() { }
        public MemberVO(string id, string pw, string name, string phoneNumber, string postalCode, string address, string email, string sex, string birthDay)
        {
            this.id = id;
            this.pw = pw;
            this.name = name;
            this.PhoneNumber = phoneNumber;
            this.postalCode = postalCode;
            this.address = address;
            this.email = email;
            this.sex = sex;
            this.brithDay = birthDay;
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Pw
        {
            get { return pw; }
            set { pw = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string PostalCode
        {
            get { return postalCode; }
            set { postalCode = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Sex
        {
            get { return sex; }
            set { sex = value; }
        }

        public string BrithDay
        {
            get { return brithDay; }
            set { brithDay = value; }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }
    }
}
