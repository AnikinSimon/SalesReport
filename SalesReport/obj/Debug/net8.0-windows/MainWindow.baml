   M S B A M L   `   `   `  ����F  BSalesReport, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:SalesReportSalesReport  5   	       SalesReport.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:SalesReport     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      � $��15q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   .+�5      �� $��Тип техники:��6   $��Center=�6+   $	��100��6F   �� 5      -   5         ��Name$ cbDeviceType��5      $	��200��6-   .@�5      �� $!��Все устройства��6    �� 
IsSelected$
 True��68   �� 5      $��Ноутбуки��6   �� 5      $��Смартфоны��6   �� 5      $��Планшеты��6   5      5      �� 5   
   $��1��6   $ 
Horizontal=�6"   $��0,10,0,0q�6;   .+�5       �� $ ��Период отчета:��6   $��Center=�6-   $	��100��6H   �� 5!      -   5        $ cbReportPeriod��5!      $	��200��6/   .@�5"      �� $��День��6   �� 5#      $��Неделя��6   �� 5$      $��
Месяц��6   $
 True��6/   �� 5%      $��Квартал��6   �� 5&      $��Год��6   5'      5(      �� 5+   
   $��2��6   $ 
Horizontal=�6"   $��0,10,0,0q�6;   .+�5,      �� $��Дата отчета:��6   $��Center=�6+   $	��100��6F   (  "System.Windows.Controls.DatePicker  5-      -   5          Name$ dpReportDate��5-      $	��200��6/   5.      �� 51   
   $��3��6   $%��Доступные отчеты��6    $��	0,15,0,80q�6:   $��2��6M   .��52      �� -   5         ��Name$ lbAvailableReports��52       �� SelectionMode$ Multiple=�62   $��0,0,0,6q�6K   ��53      �� 54      .R�55      �� -   5        ��55   #   �� *BaseReport.Name+ d� 	IsChecked 6G   �� *
IsSelected+6h   56      58      59      �� 5<   
   $��4��6   $ 
Horizontal=�6"   $��	0,185,0,0q�6;   .+�5=      �� $(��"Формат сохранения:��6   $��Center=�61   $	��140��6L   �� 5>      -   5        $ cbSaveFormat��5>      $��75��6-   $��25��68   .@�5?      �� $
��JSON��6   $
 True��6.   �� 5@      $	��XML��6   5A      5B      �� 5E   
   $��5��6   $ 
Horizontal=�6"   $��Right=�6;   $��0,15,0,0q�6W   .+�5F      �� -   5        	 ��Name$	 btnShowReport��5F      $!��Показать отчет��6,   $	��120��6E   $��0,0,10,0q�6Q   ${�False��6c   5G      5I      5J      