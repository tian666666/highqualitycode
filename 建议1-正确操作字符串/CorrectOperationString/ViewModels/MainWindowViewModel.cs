using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorrectOperationString.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        /// <summary>
        /// 1.确保尽量少的装箱
        /// 2.避免分配额外的内存空间
        ///     ①拼接字符串时 应当避免使用操作符“+” string strA = strB + strC。×
        ///     ②字符串拼接使用string.Format方法。√
        /// </summary>


        public MainWindowViewModel()
        {
            Text = "Hello World !";
        }
        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                SetProperty(ref _text, value);
            }
        }

        private DelegateCommand _clickCommand;
        public DelegateCommand ClickCommand => _clickCommand ?? (_clickCommand = new DelegateCommand(ExecuteClickCommand));

        private void ExecuteClickCommand()
        {
            //string对象（字符串对象）是个很特殊的对象，它一旦被赋值就不可改变。
            //在运行时调用System.String类中的任何方法或进行任何运算（如“=”赋值、“+”拼接等），
            //都会在内存中创建一个新的字符串对象。
            //这意味着要为该新对象分配新的内存空间。

            string s1 = "abc";
            s1 = "123" + s1 + "456";

            //上面两行代码 创建了3个字符串对象 并执行了一次string.Contact方法


            string s2 = 9 + "456";


            //上面这行代码发生一次装箱，并调用一次string.Contact方法


            string s3 = "123" + "abc" + "456";

            //上面这行代码 等效于 string s3 = "123abc456"; //此行代码 不会在运行时拼接字符串 而是会在编译时直接生成一个字符串

            const string a = "t";
            string s4 = "abc" + a;


            //上面这行代码 等效于 string s4 = "abc" + "t"; //等效于 string s4 = "abct"; //此行代码 不会在运行时拼接字符串 而是会在编译时直接生成一个字符串

            //System.String类会在某些场合带来明显的性能损耗，StringBuilder来弥补String的不足。
            //StringBuilder不会重新创建一个string对象，它的效率源于预先以非托管的方式分配内存。

            string s5 = "t";
            s5 += "e";
            s5 += "s";
            s5 += "t";

            string s6 = "t";
            string s7 = "e";
            string s8 = "s";
            string s9 = "t";
            string s10 = s6 + s7 + s8 + s9;

            //无论是 s5的拼接方法 还是s10的拼接方法 以上两种方法的效率都不高。

            //要【在运行时】完成这样的字符串拼接，更好的做法是使用StringBuilder,如下：
            
            StringBuilder stringBuilderResult = new StringBuilder(s6);
            stringBuilderResult.Append(s7);
            stringBuilderResult.Append(s8);
            stringBuilderResult.Append(s9);

            //更简化的方法是 使用string.Format方法。string.Format方法在内部使用StringBuilder进行字符串的格式化。
            
            string.Format("{0}{1}{2}{3}", s6, s7, s8, s9);


            //获取结果
            
            string formatResult = string.Format("{0}{1}{2}{3}", s6, s7, s8, s9);


            //终极建议：字符串拼接 使用string.Format
            
            Text = string.Format("{0}{1}{2}{3}{4}{5}{6}", "Hello World!", "\n"  ,"Hello WPF!","\n","Hello Prism！","\n","Hello .Net Core!");
        }
    }
}
