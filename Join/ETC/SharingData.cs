using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Join
{
    class SharingData
    {
        private static SharingData sData;
        private List<MemberVO> memberList;

        public SharingData()
        {
            MemberList = new List<MemberVO>();
        }

        public static SharingData GetInstance()
        {
            if (sData == null) sData = new SharingData();
            return sData;
        }

        internal List<MemberVO> MemberList
        {
            get { return memberList; }
            set { memberList = value; }
        }

    }


}
