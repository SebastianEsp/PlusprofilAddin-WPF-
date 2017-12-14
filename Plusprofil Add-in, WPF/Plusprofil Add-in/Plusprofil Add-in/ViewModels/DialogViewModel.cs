using EA;
using PlusprofilAddin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using PDefinitions = PlusprofilAddin.PlusprofilTaggedValueDefinitions;

namespace PlusprofilAddin.ViewModels
{
    abstract class DialogViewModel
    {
        public Repository Repository { get; set; }
        abstract public void Initialize();

        protected List<PlusprofilTaggedValue> ToAddDanishTaggedValues = new List<PlusprofilTaggedValue>
        {
            PDefinitions.prefLabelDa,
            PDefinitions.altLabelDa,
            PDefinitions.deprecatedLabelDa,
            PDefinitions.definitionDa,
            PDefinitions.exampleDa,
            PDefinitions.commentDa,
            PDefinitions.applicationNoteDa
        };
        protected List<PlusprofilTaggedValue> ToAddEnglishTaggedValues = new List<PlusprofilTaggedValue>
        {
            PDefinitions.prefLabelEn,
            PDefinitions.altLabelEn,
            PDefinitions.deprecatedLabelEn,
            PDefinitions.definitionEn,
            PDefinitions.exampleEn,
            PDefinitions.commentEn,
            PDefinitions.applicationNoteEn
        };
        protected List<PlusprofilTaggedValue> ToAddProvenanceTaggedValues = new List<PlusprofilTaggedValue>
        {
            PDefinitions.legalSource,
            PDefinitions.source,
            PDefinitions.isDefinedBy,
            PDefinitions.wasDerivedFrom
        };

        protected List<dynamic> RetrieveTaggedValues(List<dynamic> taggedValueList, string taggedValueName)
        {
            List<dynamic> result = new List<dynamic>();
            if (taggedValueList.Count == 0) throw new Exception("Object has no tagged values");
            if(taggedValueList.First().ObjectType == (int)ObjectType.otRoleTag)
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
