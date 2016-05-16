namespace StkGenCode.Code.Template
{
    public class PageService : CodeBase
    {
        public override void Gen()
        {
            //_FileName = new FileName();
            //_FileName._TableName = _TableName;
            //_FileName._ds = _ds;
            InnitProperties();

            //code = "<%@ WebService Language=\"C#\" CodeBehind=\""+ _FileName.PageServiceCodeBehideName()+ "\" Class=\""+_TableName+"Service\" %>";
            var code = "<%@ WebService Language=\"C#\" CodeBehind=\"~/App_Code/" + _FileName.PageServiceCodeBehideName() + "\" Class=\"" + _TableName + "Service\" %>";
            _FileCode.writeFile(_FileName.PageServiceName(), code);
        }
    }
}