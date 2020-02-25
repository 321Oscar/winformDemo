using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.CSharp;

namespace WindowsFormsApp1
{
    public class PrintData
    {
        //打印条码
        public void PrintBarcode(string lotId,string templateName,string defId = null,string defName = null,string defDesc = null,string boxingNum = null,string createOn = null)
        {

            string path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Data\\"+templateName+".txt";
            string printData = Read(path);
            printData = printData.Replace("@BARCODE", lotId);
            if (templateName != "004")
            {
                //string imagePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Data\\Package.png";
                //imagePath = imagePath.Replace("\\", @"\\");
                printData = printData.Replace("@DEFID", defId);
                printData = printData.Replace("@DEFNAME", defName);
                //printData = printData.Replace("@DEFDESC", defDesc);
                //printData = printData.Replace("@BOXINGNUM", boxingNum);
                //printData = printData.Replace("@CREATEON", createOn);
                //printData = printData.Replace("@IMAGE", imagePath);
            }
            AddPrintTpl(printData);
            //this.AginPrintData.Add(printData);
            //LODOP_PRINT(printData, lotId, "1", "1", new List<string>());


        }

        public string Read(string path)
        {
            StringBuilder stringBuilder = new StringBuilder();
            StreamReader sr = new StreamReader(path, Encoding.Default);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                stringBuilder.Append(line);
            }
            return stringBuilder.ToString();
        }
        private ICodeCompiler _iCodeCompiler;

      
        public ICodeCompiler CodeCompiler
        {
            get
            {
                if (_iCodeCompiler != null) return _iCodeCompiler;
                CSharpCodeProvider objCSharpCodePrivoder = new CSharpCodeProvider();
                // 2.ICodeComplier编译器实例
                _iCodeCompiler = objCSharpCodePrivoder.CreateCompiler();
                return _iCodeCompiler;
            }
        }

        private CompilerParameters _objCompilerParameters;
        public CompilerParameters ObjCompilerParameters
        {
            get
            {
                if (_objCompilerParameters == null)
                {
                    // 3.CompilerParameters
                    _objCompilerParameters = new CompilerParameters();
                    string lodopAdd = System.Windows.Forms.Application.StartupPath+@"\Lodop.dll";
                    string fileAdd = typeof(System.Xml.Linq.Extensions).Assembly.Location;
                    _objCompilerParameters.ReferencedAssemblies.Add("System.dll");
                    _objCompilerParameters.ReferencedAssemblies.Add(lodopAdd);
                    //_objCompilerParameters.ReferencedAssemblies.Add("Lodop.dll");
                    _objCompilerParameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
                    _objCompilerParameters.GenerateExecutable = false;
                    _objCompilerParameters.GenerateInMemory = true;
                }
                return _objCompilerParameters;
            }
        }

        private Dictionary<string, PrintCodeObj> _printCodeObjDic;
        private Dictionary<string, PrintCodeObj> PrintCodeObjDic
        {
            get { return _printCodeObjDic ?? (_printCodeObjDic = new Dictionary<string, PrintCodeObj>()); }
        }

        private class PrintCodeObj
        {
            public Type PrintCodeType { get; set; }
            public object DestObj { get; set; }
        }

     
        private void AddPrintTpl(string printData)
        {
            if (!PrintCodeObjDic.ContainsKey(printData))
            {
                PrintCodeObj printCodeObj = new PrintCodeObj();
                // 4.CompilerResults
                string str = GenerateCode(printData);
                //str = str.Replace("'", "\"");
                CompilerResults cr = CodeCompiler.CompileAssemblyFromSource(ObjCompilerParameters, str);
                if (cr.Errors.HasErrors)
                {
                    //MessageBox.Show("编译错误：");
                    foreach (CompilerError err in cr.Errors)
                    {
                        MessageBox.Show(err.ErrorText);
                    }
                }
                else
                {
                    Assembly objAssembly = cr.CompiledAssembly;
                    printCodeObj.PrintCodeType = objAssembly.GetType("DynamicCodeGenerate.LodopClass");
                    printCodeObj.DestObj = printCodeObj.PrintCodeType.InvokeMember(null, BindingFlags.CreateInstance, null, null, null);
                    printCodeObj.PrintCodeType.InvokeMember("LodopStrExec", BindingFlags.InvokeMethod, null,
                    printCodeObj.DestObj,new object[]{"","",""});
                }
                PrintCodeObjDic.Add(printData, printCodeObj);
            }
        }

        private void AddPrintTpl(string printData, List<string> param)
        {
            if (!PrintCodeObjDic.ContainsKey(printData))
            {
                PrintCodeObj printCodeObj = new PrintCodeObj();
                // 4.CompilerResults
                string str = GenerateCode(printData, param);
                str = str.Replace("'", "\"");
                CompilerResults cr = CodeCompiler.CompileAssemblyFromSource(ObjCompilerParameters, str);
                if (cr.Errors.HasErrors)
                {
                    //MessageBox.Show("编译错误：");
                    //foreach (CompilerError err in cr.Errors)
                    //{
                    //    MessageBox.Show(err.ErrorText);
                    //}
                }
                else
                {
                    Assembly objAssembly = cr.CompiledAssembly;
                    printCodeObj.PrintCodeType = objAssembly.GetType("DynamicCodeGenerate.LodopClass");
                    printCodeObj.DestObj = printCodeObj.PrintCodeType.InvokeMember(null, BindingFlags.CreateInstance, null, null, null);
                }
                PrintCodeObjDic.Add(printData, printCodeObj);
            }
        }

        //private void DoPrintAsync(Object sender, DoWorkEventArgs e)
        //{
        //    PrintaParam param = e.Argument as PrintaParam;
        //    if (param == null) return;
        //    PrintCodeObj printCodeObj = PrintCodeObjDic[param.PrintData];
        //    List<object> objectList = new List<object> { param.Barcode, param.Package_cout, param.PackageNum };
        //    objectList.AddRange(param.ValueList);
        //    printCodeObj.PrintCodeType.InvokeMember("LodopStrExec", BindingFlags.InvokeMethod, null, printCodeObj.DestObj, objectList.ToArray());
        //}

        private class PrintaParam
        {
            public string PrintData { get; set; }
            public string Barcode { get; set; }
            public string Package_cout { get; set; }
            public string PackageNum { get; set; }
            public List<string> ValueList { get; set; }
        }

        private void LODOP_PRINT(string printData, string barcode, string package_cout, string packageNum, List<string> paramList)
        {
            //BackgroundWorker bw = new BackgroundWorker();
            //bw.DoWork += new DoWorkEventHandler(DoPrintAsync);
            //bw.RunWorkerAsync(new PrintaParam() { PrintData = printData, Barcode = barcode, PackageNum = packageNum, Package_cout = package_cout, ValueList = paramList });
            Thread threadPrint = null;
            threadPrint = new Thread(delegate()
            {
                PrintCodeObj printCodeObj = PrintCodeObjDic[printData];
                List<object> objectList = new List<object>
                {
                    barcode,
                    package_cout,
                    packageNum
                };
                objectList.AddRange(paramList.Cast<object>());
                printCodeObj.PrintCodeType.InvokeMember("LodopStrExec", BindingFlags.InvokeMethod, null,
                    printCodeObj.DestObj, objectList.ToArray());
            }) { IsBackground = true };
            threadPrint.Start();
        }

        public static string GenerateCode(string labelTemplateStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("using System;");
            sb.Append("using Lodop;");
            sb.Append("using System.Windows.Forms;");
            sb.Append(Environment.NewLine);
            sb.Append("namespace DynamicCodeGenerate");
            sb.Append(Environment.NewLine);
            sb.Append("{");
            sb.Append(Environment.NewLine);
            sb.Append("      public class LodopClass");
            sb.Append(Environment.NewLine);
            sb.Append("      {");
            sb.Append(Environment.NewLine);
            sb.Append("          public static LodopX LODOP = new LodopX();");
            sb.Append("          public static Object newObject = new Object();");
            sb.Append("          public void LodopStrExec(string barcode,string package_cout,string packageNum)");
            sb.Append(Environment.NewLine);
            sb.Append("          {");
            sb.Append(Environment.NewLine);
            sb.Append("               lock(newObject)");
            sb.Append("               {");
            sb.Append(Environment.NewLine);
            sb.Append("                LODOP.SET_LICENSES('杭州集控软件有限公司', '8973E9D5AB0D6676F777D05CE072D5A1', '', '');");
            sb =    sb.Replace("'", "\"");
            sb.Append(Environment.NewLine);
            sb.Append("               " + labelTemplateStr);
            sb.Append(Environment.NewLine);
            sb.Append("               LODOP.PRINT();");
            sb.Append("                }");
            sb.Append(Environment.NewLine);
            sb.Append("          }");
            sb.Append(Environment.NewLine);
            sb.Append("      }");
            sb.Append(Environment.NewLine);
            sb.Append("}");
            string code = sb.ToString();
            return code;
        }

        public static string GenerateCode(string labelTemplateStr, List<string> param)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("using System;");
            sb.Append("using System.Windows.Forms;");
            sb.Append("using Lodop;");
            sb.Append(Environment.NewLine);
            sb.Append("namespace DynamicCodeGenerate");
            sb.Append(Environment.NewLine);
            sb.Append("{");
            sb.Append(Environment.NewLine);
            sb.Append("      public class LodopClass");
            sb.Append(Environment.NewLine);
            sb.Append("      {");
            sb.Append(Environment.NewLine);
            sb.Append("          public static LodopX LODOP = new LodopX();");
            sb.Append("          public static Object newObject = new Object();");
            sb.Append("          public void LodopStrExec(string barcode,string package_cout,string packageNum");
            if (param.Count > 0)
            {
                foreach (string singleParam in param)
                {
                    labelTemplateStr = labelTemplateStr.Replace("\"" + singleParam + "\"", singleParam);
                    labelTemplateStr = labelTemplateStr.Replace("\"" + singleParam + "-", singleParam + "+ \"-\" +");
                    labelTemplateStr = labelTemplateStr.Replace(singleParam + "\"", singleParam);
                    sb.Append(",");
                    sb.Append("string " + singleParam);
                }
            }
            sb.Append(")");
            sb.Append(Environment.NewLine);
            sb.Append("          {");
            sb.Append(Environment.NewLine);
            sb.Append("             try{");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("               lock(newObject)");
            sb.Append("               {");
            sb.Append("                LODOP.PRINT_INIT('打印控件功能演示_Lodop功能_表单一');");
            sb.Append("                LODOP.SET_LICENSES('杭州集控软件有限公司', '8973E9D5AB0D6676F777D05CE072D5A1', '', '');");

            sb.Append(Environment.NewLine);
            sb.Append("               " + labelTemplateStr);
            sb.Append("               object result = LODOP.PRINT();");
            sb.Append(Environment.NewLine);
            sb.Append("               ");
            sb.Append(Environment.NewLine);
            sb.Append("               string isPrintSucess = Convert.ToString(result);");
            sb.Append(Environment.NewLine);
            //sb.Append("               if(isPrintSucess.ToUpper()!=\"TRUE\"){this.LodopStrExec(barcode,package_cout,packageNum");
            //if (param.Count > 0)
            //{
            //    foreach (string singleParam in param)
            //    {
            //        labelTemplateStr = labelTemplateStr.Replace("\"" + singleParam + "\"", singleParam);
            //        labelTemplateStr = labelTemplateStr.Replace("\"" + singleParam + "-", singleParam + "+ \"-\" +");
            //        labelTemplateStr = labelTemplateStr.Replace(singleParam + "\"", singleParam);
            //        sb.Append(",");
            //        sb.Append(" " + singleParam);
            //    }
            //}
            //sb.Append(");}");
            sb.Append(Environment.NewLine);
            sb.Append("                }");
            sb.Append("                }");

            sb.Append(Environment.NewLine);
            sb.Append("                catch (Exception e)");
            sb.Append("                {");
            sb.Append("                 return;");
            sb.Append("                }");
            sb.Append("          }");
            sb.Append(Environment.NewLine);
            sb.Append("      }");
            sb.Append(Environment.NewLine);
            sb.Append("}");
            string code = sb.ToString();
            return code;
        }
    }
}
