using EA;
using System;
using System.Collections.Generic;
using System.Windows;

namespace PlusprofilAddin.ViewModels
{
    abstract class DialogViewModel
    {
        public Repository Repository { get; set; }
        abstract public void Initialize();

        protected List<string> ToAddDanishTaggedValues = new List<string>
        {
            "prefLabel (da)",
            "altLabel (da)",
            "deprecatedLabel (da)",
            "definition (da)",
            "example (da)",
            "comment (da)",
            "applicationNote (da)"
        };
        protected List<string> ToAddEnglishTaggedValues = new List<string>
        {
            "prefLabel (en)",
            "altLabel (en)",
            "deprecatedLabel (en)",
            "definition (en)",
            "example (en)",
            "comment (en)",
            "applicationNote (en)"
        };
        protected List<string> ToAddProvenanceTaggedValues = new List<string>
        {
            "legalSource",
            "source",
            "isDefinedBy",
            "wasDerivedFrom"
        };

        protected List<dynamic> RetrieveTaggedValues(List<dynamic> taggedValueList, string taggedValueName)
        {
            List<dynamic> result = new List<dynamic>();
            if (taggedValueList.Count == 0) throw new Exception("No tagged values were found");
            if(taggedValueList[0].ObjectType == (int)ObjectType.otRoleTag)
            {
                foreach (dynamic tv in taggedValueList)
                {
                    if (tv.Tag == taggedValueName) result.Add(tv);
                }
            }
            else
            {
                foreach (dynamic tv in taggedValueList)
                {
                    if (tv.Name == taggedValueName) result.Add(tv);
                }
            }
            if(result.Count == 0) throw new Exception("No tagged value with name " + taggedValueName + " was found");
            return result;
        }
    }
}
