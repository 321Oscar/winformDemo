# winformDemo

## DataGridView使用
1.选中全行 selectionMode  
2.行头显示：RowHeaderVisible   
2.1 在行头上绘制数字，实现增加序号的功能。   
2.2 绑定数据源后 选中行转为实体。   
T entity = datagridview.currentRow.DataBoundItem as T;    
3.DateTime转换  
DateTime.ParseExact(string,format,System.Globalization.CultureInfo.InvarianCulture);
