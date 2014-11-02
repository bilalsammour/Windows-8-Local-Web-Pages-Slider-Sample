This sample show how to use WebView in MVVM technique (data binding), WebView open a local web page that can contains images, CSSs, JSs, and anything.


This sample contains how to:

- Use WebView in DataBanding
- Open locan websit in WebView using IUriToStreamResolver interface.
- Custom Pivot
- Use FlipView.
- Implement Templates.
- Create CustomControl.
- Parse JSON using Serialization
- Use StorageFlle and StorageFolder.
- Imlement BottomAppBar (AppBar in Windows Phone 8.0).
 

Note
---

- For any web content, Build Action must be Content
 

ArticleControl
---

A custom control that has WebView on it.


Binding ArticleControl to Pivot (XAML)
---

```sh
<Pivot x:Name="allArticles" ItemsSource="{Binding WebPages}" ItemTemplate="{StaticResource OneArticleTemplate}" Style="{StaticResource PivotWithoutTabsStyle}"/>
```
 
Binding ArticleControl to FlipView (XAML)
---


```sh
<FlipView Grid.Row="1" ItemsSource="{Binding WebPages}" ItemTemplate="{StaticResource OneArticleTemplate}"/>
```

Operating system requirements
---

- Windows 8.1
- Windows Phone 8.1

Developed By
------------

Bilal Sammour - bilal@startappz.com


License
-------

    Copyright 2014 Bilal Sammour
    
    Licensed under the Apache License, Version 2.0 (the "License");
    you may not use this file except in compliance with the License.
    You may obtain a copy of the License at
    
    http://www.apache.org/licenses/LICENSE-2.0
    
    Unless required by applicable law or agreed to in writing, software
    distributed under the License is distributed on an "AS IS" BASIS,
    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    See the License for the specific language governing permissions and
    limitations under the License.

