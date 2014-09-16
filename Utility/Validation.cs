using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Utility
{
    public interface IBlankable
    {
        string Text
        {
            get;
        }

    }
    public class Validations
    {
       
        public static ErrorProvider ep = new ErrorProvider();

        public static bool IsNotBlankAll(Control ctl)
        {
            ep.Clear();
            List<Control> controlList = new List<Control>();
            if (ctl is TextBox)
            {
                if (((TextBox)ctl).Text.Trim().Equals(string.Empty))
                {
                    controlList.Add(ctl);
                }
            }
            if (ctl is ComboBox)
            {
                if (((ComboBox)ctl).Text.Trim().Equals(string.Empty))
                {
                    controlList.Add(ctl);
                }
            }

            LoadBlankControl(controlList, ctl);
            if (controlList.Count == 0)
            {
                return true;
            }
            else
            {
                foreach (Control c in controlList)
                {
                    ep.SetError(c, "Blank value not allowed !");
                }
                return false;
            }
        }
        public static bool IsNotBlankExcept(Control ctl, params Control[] controlList)
        {
            ep.Clear();
            List<Control> controls = controlList.ToList<Control>();
            List<Control> tmpControlList = new List<Control>();

            LoadBlankControl(tmpControlList, ctl, controls);
            foreach (Control t in controls)
            {
                if (IsExist(tmpControlList, t))
                {
                    Delete(tmpControlList, t);
                }
            }
            if (tmpControlList.Count == 0)
            {
                return true;
            }
            else
            {
                ep.Clear();
                foreach (Control c in tmpControlList)
                {
                    ep.SetError(c, "Blank value not allowed !");
                }
                return false;
            }
        }
        public static bool IsExistValue(ComboBox cmb, string value)
        {
            foreach (var v in cmb.Items)
            {
                if (v.ToString().Equals(value))
                {
                    return true;
                }
            }
            return false;

        }
        #region Class Private methods ----------------------------------------------------
        private static void Delete(List<Control> controlList, Control c)
        {
            foreach (Control t in controlList)
            {
                if (t == c)
                {

                    controlList.Remove(t);
                    break;
                }
            }
        }
        private static bool IsExist(List<Control> controlList, Control c)
        {
            foreach (Control t in controlList)
            {
                if (t == c) return true;
            }
            return false;
        }
        private static void LoadBlankControl(List<Control> coltrolList, Control c, List<Control> ignorControls)
        {
            foreach (Control t in c.Controls)
            {
                if (IsExist(ignorControls, t))
                {
                    continue;
                }
                if (t is TextBox)
                {
                    if (((TextBox)t).Text.Trim() == "")
                    {
                        coltrolList.Add(t);
                    }
                }
                if (t is ComboBox)
                {
                    if (((ComboBox)t).Text.Trim() == "")
                    {
                        coltrolList.Add(t);
                    }
                }
                else if (t is IBlankable)
                {
                    if (((IBlankable)t).Text.Trim() == "")
                    {
                        coltrolList.Add(t);
                    }
                }
                else
                {
                    if (t.Controls.Count > 0)
                    {
                        LoadBlankControl(coltrolList, t, ignorControls);
                    }
                }
            }
        }

        private static void LoadBlankControl(List<Control> coltrolList, Control c)
        {
            foreach (Control t in c.Controls)
            {

                if (t is TextBox)
                {
                    if (((TextBox)t).Text.Trim() == "")
                    {
                        coltrolList.Add(t);
                    }
                }
                if (t is ComboBox)
                {
                    if (((ComboBox)t).Text.Trim() == "")
                    {
                        coltrolList.Add(t);
                    }
                }
                else if (t is IBlankable)
                {
                    if (((IBlankable)t).Text.Trim() == "")
                    {
                        coltrolList.Add(t);
                    }
                }
                else
                {
                    if (t.Controls.Count > 0)
                    {
                        LoadBlankControl(coltrolList, t);
                    }
                }
            }
        }
        #endregion
    }
}
