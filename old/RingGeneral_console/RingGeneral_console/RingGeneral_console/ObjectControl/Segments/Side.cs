using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingGeneral_console
{
    class Side
    {
        public List<Member> MemberList = new List<Member>();

        public string SideName { get; set; }
        bool ManualName;

        public bool? Winner { get; set; }

        public Side(string name)
        {
            if (name != "None")
                ManualName = true;
            else
                SideName = name;
        }

        public Side() : this("None")
        {
        }

        public void UpdateName()
        {
            SideName = GetName();
        }

        public void AddMember(Member member)
        {
            MemberList.Add(member);
            if (!ManualName)
                UpdateName();
        }

        public string GetName()
        {
            StringBuilder nameString = new StringBuilder();
            if (MemberList.Count == 1)
                return MemberList[0].MemberName;
            if (MemberList.Count == 2)
                return string.Format("{0} and {1}", MemberList[0].MemberName, MemberList[1].MemberName);
            else
            {
                for (int name = 0; name < MemberList.Count; name++)
                {
                    if (name == MemberList.Count - 1)
                        nameString.AppendFormat("and {0}", MemberList[name].MemberName);
                    else
                    {
                        nameString.AppendFormat("{0}, ", MemberList[name].MemberName);
                    }
                }
                return nameString.ToString();
            }
        }

        public override string ToString()
        {
            return SideName;
        }
    }
}
