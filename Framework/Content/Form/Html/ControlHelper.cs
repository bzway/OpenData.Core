﻿using Autofac;
using OpenData.Common;
using OpenData.Common.AppEngine;
using OpenData.Framework.Common;
using OpenData.Framework.Common.Form.Html.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpenData.Framework.Common.Form.Html
{
    public static class ControlHelper
    {
        #region Static
        static string TemplateDir = "";
        static string TemplateVirtualPath = "";
        static IDictionary<string, Controls.IControl> controls = new Dictionary<string, IControl>(StringComparer.CurrentCultureIgnoreCase);

        static ControlHelper()
        {
            var baseDir = ApplicationEngine.Current.Default.Resolve<IBaseDir>();
            TemplateDir = Path.Combine(baseDir.AppDataPhysicalPath, "ContentType_Templates", "Controls");
            TemplateVirtualPath = UrlUtility.Combine(baseDir.AppDataVirutalPath, "ContentType_Templates", "Controls");

            RegisterControl(new TextBox());
            RegisterControl(new InputInt32());
            RegisterControl(new InputFloat());
            RegisterControl(new CheckBox());
            RegisterControl(new Date());
            RegisterControl(new OpenData.Framework.Common.Form.Html.Controls.File());
            //RegisterControl(new ImageCrop());
            RegisterControl(new Display());
            RegisterControl(new Hidden());
            RegisterControl(new Empty());
            RegisterControl(new DropDownList());
            RegisterControl(new CheckBoxList());
            RegisterControl(new RadioList());
            //RegisterControl(new MultiFiles());
            RegisterControl(new TextArea());
            RegisterControl(new Tinymce());
            RegisterControl(new HighlightEditor());
            RegisterControl(new Password());
            //RegisterControl(new InputNumber());
            //RegisterControl(new CLEditor());
        }
        #endregion

        #region RegisterControl
        public static void RegisterControl(IControl control)
        {
            controls[control.Name] = control;
        }

        public static bool Contains(string controlType)
        {
            return ResolveAll().Where(it => string.Compare(it, controlType, true) == 0).Count() > 0;
        }
        #endregion

        #region Resolve
        public static IControl Resolve(string controlType)
        {
            IControl control = null;
            if (controls.ContainsKey(controlType))
            {
                control = controls[controlType];
            }
            return control;
        }
        #endregion

        #region ResolveAll
        public static IEnumerable<string> ResolveAll()
        {
            var controlNames = controls.Keys.AsEnumerable();


            if (OpenData.Framework.Common.TrustLevelUtility.CurrentTrustLevel == AspNetHostingPermissionLevel.Unrestricted)
            {
                if (Directory.Exists(TemplateDir))
                {
                    var files = Directory.EnumerateFiles(TemplateDir, "*.cshtml");
                    controlNames = controlNames.Concat(files.Select(it => Path.GetFileNameWithoutExtension(it)))
                       .Distinct(StringComparer.CurrentCultureIgnoreCase);
                }
            }
            return controlNames;
        }
        #endregion

        #region Render
        static string AllowedEditWraper(string template)
        {
            var result = string.Empty;
            result = string.Format(@"
            @if (allowedEdit) {{
                {0}
            }}", template);
            return result;
        }


        public static string Render(this IColumn column, ISchema schema, bool isUpdate)
        {
            var controlType = column.ControlType;
            if (isUpdate && !column.Modifiable)
            {
                controlType = "Hidden";
            }
            if (string.IsNullOrEmpty(controlType))
            {
                return string.Empty;
            }
            if (!Contains(controlType))
            {
                throw new Exception(string.Format("Control type {0} does not exists.", controlType));
            }
            bool rendered = false;
            string controlHtml = ProcessRazorView(schema, column, out rendered);
            if (!rendered && controls.ContainsKey(controlType))
            {
                controlHtml = controls[controlType].Render(schema, column);
            }

            if (string.Equals(column.Name, "published", StringComparison.OrdinalIgnoreCase))
            {
                controlHtml = AllowedEditWraper(controlHtml);
            }

            return controlHtml;
        }
        #endregion

        #region Razor view
        private static string GetControlView(string controlType)
        {
            return Path.Combine(TemplateDir, controlType + ".cshtml");
        }
        private static string GetControlViewVirtualPath(string controlType)
        {
            return UrlUtility.Combine(TemplateVirtualPath, controlType + ".cshtml");
        }
        public static string ProcessRazorView(ISchema schema, IColumn column, out bool rendered)
        {
            string controlFile = GetControlView(column.ControlType);
            rendered = false;
            if (System.IO.File.Exists(controlFile))
            {
                if (HttpContext.Current != null && HttpContext.Current.Items["ControllerContext"] != null)
                {
                    var controllerContext = (ControllerContext)HttpContext.Current.Items["ControllerContext"];
                    var viewData = new ViewDataDictionary() { Model = column };
                    viewData["Schema"] = schema;
                    rendered = true;
                    return RazorViewHelper.RenderView(GetControlViewVirtualPath(column.ControlType), controllerContext, viewData);
                }
            }

            return "";
        }
        #endregion
    }
}
