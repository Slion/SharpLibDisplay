using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpLib.Display
{
    /// <summary>
    /// Define a view to be rendered on our display.
    /// TODO: Should this be used by our client too?
    /// </summary>
    class View
    {
        public View()
        {
            Fields = new List<DataField>();
            Layout = new TableLayout(1, 2); //Default to one column and two rows
        }

        public List<DataField> Fields { get; set; }
        public TableLayout Layout { get; set; }

        /// <summary>
        /// Look up the corresponding field in our field collection.
        /// </summary>
        /// <param name="aField"></param>
        /// <returns></returns>
        public DataField FindSameFieldAs(DataField aField)
        {
            foreach (DataField field in Fields)
            {
                if (field.IsSameAs(aField))
                {
                    return field;
                }
            }

            return null;
        }


        /// <summary>
        /// Look up the corresponding field in our field collection.
        /// </summary>
        /// <param name="aField"></param>
        /// <returns></returns>
        public int FindSameFieldIndex(DataField aField)
        {
            int i = 0;
            foreach (DataField field in Fields)
            {
                if (field.IsSameAs(aField))
                {
                    return i;
                }
                i++;
            }

            return -1;
        }
    }
}
