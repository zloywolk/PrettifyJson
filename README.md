# Prettify Json Viewer

WPF user control, позволяющий серелиазовать любой объект в JSON (используется Newtonsoft.JSON) и просматривать его в виде дерева на GUI интерфейсе.

**Пример**

```xaml
<Window
  xmlns:pc="clr-namespace:Pinix.Windows.Controls;assembly=Pinix.Windows.Controls"
  <!-- Grid definitions, another content -->
  <Grid>
    <pc:PrettifyJsonViewer Grid.Row="2" DataContext="{Binding Users}"
                           IsExpanded="True"
                           DoublePrecision="5"
                           ShowArrayLength="True"
                           StringBrush="Brown"
                           IntegerBrush="Blue"
                           FloatBrush="Purple"
                           PropertyBrush="Black"
                           PropertyFontWeight="Bold"
                           ShowIcons="True"
                           ExpandButtonStyle="{StaticResource ToggleButton.TriangleExpandCollapseStyle}"
                           ConnectingLines="True"/>
    <!-- content -->
  </Grid>
</Window>
```

**v0.1.4**

* Иконки для массивов, свойств и объектов
* Пользовательский цвет для натуральных (целых) чисел, чисел с плавающей точкой и строк
* Пользовательский стиль для ExpandButton в TreeViewItem (свойство ```ExpandButtonStyle```)
* Connecting lines (как в WinForms TreeView)
* Число десятичных знаков после запятой для чисел с плавающей точкой
