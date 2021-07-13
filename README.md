# 说明  
此项目为后台管理模板   
## 模板详细
WebApi  
Vue  
Element  
## 操作说明  
下载Release版本 `WebApiTpl.zip`
解压目录  
例如目录为  ` "C:\Users\zhf\source\repos\WebApiTpl\WebApiTpl"` 
## 安装  
cmd运行  
`dotnet new -i "C:\Users\zhf\source\repos\WebApiTpl\WebApiTpl"`
## 使用  
`dotnet new wtpl -n webName`  
说明:  
`wtpl`:模板的简写名称 在`WebApiTpl/.template.config/template.json`中配置  
`webName`:在cmd目录下新建的项目的名称  
前后端建议分离调试:  
 cd到`clientapp`目录后  
`yarn install`  
`yarn serve`  



## 代码更新 
将`WebApiTpl/content/`下面目录复制到压缩包  
注意:删除bin,obj  





