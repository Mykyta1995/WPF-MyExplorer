   M S B A M L   `   `   `  ����  WPFExplorer    WPFExplorer.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  
MyExplorer��5   	   $	��500��6   $	��800��6)    � Icon$. (Resources\icons8-windows-explorer-32.png��6m   .��5      �  � RowDefinitions 5   
   �� 5      $��25 �6   �� 5      $��90 �6   �� 5	      $��40 �6   �� 5
      �� 5      $��25 �6    � ColumnDefinitions 5   
   �� 5      $	��200 �6   �� 5      �� 5      $
��Auto �6   .+�5   
   ]� $	��Top=�6   $��0��6)   $��0��69   $��2��6F   
�������.+�5      � .+�5      o� .@�5      m� -   5        $��btnFile��5   �   $��   File��6   
����  �$��25��6B   $��70��6N   
�������$��15(�6l   
����  �.@�5      m� -   5        $
��Open��5   #   
���   � m� Icon 5      �� 5   "   $"��Resources\icons8-open-48.png��6(   $��16��6N   5      m� 5      $��Create��6#   
���   � 5      �� 5   "   $%��Resources\icons8-add-new-16.png��6(   $��16��6Q   .@�5       m� -   5        $��	Directory��5    '   
���   � 5!   "   �� 5"   &   $-��'Resources\icons8-folder-invoices-16.png��6,   5$      m� 5%      -   5        $%��Document Microsoft Windows Word��5%   '   
���   � 5&   "   �� 5'   &   $0��*Resources\icons8-microsoft-word-16 (1).png��6,   5)      m� 5*      -   5        $#��Document Microsoft PowerPoint��5*   '   
���   � 5+   "   �� 5,   &   $2��,Resources\icons8-microsoft-powerpoint-16.png��6,   5.      m� 5/      -   5        $��Text Document��5/   '   
���   � 50   "   �� 51   &   $&�� Resources\icons8-document-16.png��6,   53      54      m� 55      -   5        $��Rename��55   #   
���   � 56      �� 57   "   $$��Resources\icons8-rename-48.png��6(   $��16��6P   59      m� 5:      -	   5        $��Delete��5:   #   
���   � 5;      �� 5<   "   $$��Resources\icons8-delete-16.png��6(   $��16��6P   5>      5?      5@      �� 5A      -
   5        $��btnComputer��5A   �   $��Computer��6   
�������$��25��6C   $��80��6O   $��15(�6[   
���   �
�������6�   �� 5B      -   5        $��btnView��5B   �   $
��View��6   
�������$��25��6;   $��60��6G   
�������$��15(�6f   6�   5C      5D      �� 5E   
   -   5        $��1��5E      $��0��6!   $��Right=�6.   $��30��6J   
�������
�������.��5F      �� $*��$Resources\icons8-ask-question-48.png��6   $��25��6B   5G      � 5H   
   -   5        $��WarpComputer��5H   \   $��0��6   $��1��6$   $��3��61   
�������.+�5I      � .+�5J      �� -   5        $��btnInfo��5J   �   $��60��6   $��80��6$   $	��5,5q�60   
�������$��Center=�6U   $��Center=�6r   
�������.��5K      ��  �� Orientation$ Vertical=�6!   .+�5L      �� $#��Resources\icons8-about-48.png��6    �� 5M      $��Bottom=�6$   $��Center=�6?   Info6b   5N      5O      �� 5P      -   5        $��BtnOpen��5P   �   $��60��6   $��80��6$   $	��5,5q�60   
�������$��Center=�6U   $��Center=�6r   
�������.��5R      �� $ Vertical=�6!   .+�5S      �� $"��Resources\icons8-open-48.png��6    �� 5T      $��Bottom=�6$   $��Center=�6?   Open6b   5U      5V      �� 5W      -   5        $��	btnRename��5W   �   $��60��6   $��80��6$   $	��5,5q�60   
�������$��Center=�6U   $��Center=�6r   
�������.��5X      �� $ Vertical=�6!   .+�5Y      �� $$��Resources\icons8-rename-48.png��6    �� 5Z      $��Bottom=�6$   $��Center=�6?   Rename6d   5[      5\      5]      5^      � 5_   
   -   5        $��WarpView��5_   \   $��0��6   $��1��6$   $��2��61   
�������$y�Hidden=�6l   .+�5`      � .+�5a      �� -   5        $��	btnMedium��5a   �   $��60��6   $��80��6$   $	��5,5q�60   
�������$��Center=�6U   $��Center=�6r   
�������.��5b      �� $ Vertical=�6!   .+�5c      �� $*��$Resources\icons8-medium-icons-48.png��6    �� 5d      $��Bottom=�6$   $��Center=�6?   Medium6d   5e      5f      �� 5g      -   5        $��btnList��5g   �   $��60��6   $��80��6$   $	��5,5q�60   
�������$��Center=�6U   $��Center=�6r   
�������.��5h      �� $ Vertical=�6!   .+�5i      �� $'��!Resources\icons8-checklist-48.png��6    �� 5j      $��Bottom=�6$   $��Center=�6?   List6b   5k      5l      �� 5m      -   5        $��btnBig��5m   �   $��60��6   $��80��6$   $	��5,5q�60   
�������$��Center=�6U   $��Center=�6r   
�������.��5n      �� $ Vertical=�6!   .+�5o      �� $/��)Resources\icons8-data-sheet-filled-48.png��6    �� 5p      $��Bottom=�6$   $��Center=�6?   Table6c   5q      5r      5s      5t      ]� 5u   
   $��0��6   $��3��6$   $��Visible=�61   .+�5v      R� -   5        $��TVRoot��5v   9   
�������$��14(�6+   .@�5w      Q� -   5        $��ItemFavorites��5w      ��5x      �� 5y      $ 
Horizontal=�6%   $��Visible=�6>   .+�5z      �� $)��#Resources\icons8-star-filled-16.png��6$   $��16��6Q   �� 5{      $��15(�6(   My Favorites6D   5|      5~      Q� 5      $�� ��6   {�. 6)   Q� 5�      -   5        $��ItemComputer��5�      ��5�      �� 5�      $ 
Horizontal=�6%   $��Visible=�6>   .+�5�      �� $)��#Resources\icons8-workstation-16.png��6$   $��16��6Q   �� 5�      $��15(�6(   My Computer6C   5�      5�      5�      5�      ]� 5�   
   $��Right=�6   $��1��6+   $��3��6;   .+�5�      �� -   5        $��LVInfo��5�   ^   $��Visible=�6   
�������$��0,5,0,-5q�6�   $
��Auto��5�   G   
���   � � ContextMenu 5�      �� 5�      .@�5�      m� -   5        $
��Open��5�   #    5�      �� 5�   "   $"��Resources\icons8-open-48.png��6(   $��16��6N   5�      m� 5�      -   5        $��
MenuCriate��5�   3   $��Create��6#    5�      �� 5�   "   $%��Resources\icons8-add-new-16.png��6(   $��16��6Q   .@�5�      m� -   5        $��	Directory��5�   '    5�   "   �� 5�   &   $-��'Resources\icons8-folder-invoices-16.png��6,   5�      m� 5�      -   5        $%��Document Microsoft Windows Word��5�   '    5�   "   �� 5�   &   $0��*Resources\icons8-microsoft-word-16 (1).png��6,   5�      m� 5�      -   5        $#��Document Microsoft PowerPoint��5�   '    5�   "   �� 5�   &   $2��,Resources\icons8-microsoft-powerpoint-16.png��6,   5�      m� 5�      -   5        $��Text Document��5�   '    5�   "   �� 5�   &   $&�� Resources\icons8-document-16.png��6,   5�      5�      m� 5�      -   5        $��
MenuRename��5�   L   $��Rename��6#    5�      �� 5�   "   $$��Resources\icons8-rename-48.png��6(   $��16��6P   5�      m� 5�      -    5        $��
MenuDelete��5�   3   $��Delete��6#    5�      �� 5�   "   $$��Resources\icons8-delete-16.png��6(   $��16��6P   5�      m� 5�      -!   5        $��MenuCopy��5�   1   $
��Copy��6#    5�      �� 5�   "   $"��Resources\icons8-copy-16.png��6(   5�      m� 5�      -"   5        $��
MenuInsert��5�   3   $��Insert��6#   $y�	Collapsed=�6^    5�      �� 5�   "   $$��Resources\icons8-insert-16.png��6(   5�      5�      5�      5�      �� 5�   
   $��0��6   $��3��6'   $��1��64   
�������6>   �� 5�   
   $��0��6   $��4��6%   $��2��62   .+�5�      �� -#   5        $��lbElemet��5�   (   $��	Elements:��6   6(   5�      �� 5�   
   $��0��6   $��2��6%   $��3��62   $ 
Horizontal=�6F   .+�5�      �� -$   5        $��btnBack��5�   M   $��20��6   $��40��6!   $��10q�6,   
�������{�.  .��5�      �� $"��Resources\icons8-left-16.png��6   5�      �� 5�      -%   5        $��btnFront��5�   �   $��20��6   $��40��6!   $��
0,10,10,10q�6,   
�������{�.  .��5�      �� $#��Resources\icons8-right-16.png��6   5�      �� 5�      -&   5        $
��Path��5�   .   $��25��6   $	��480��6"   $��15(�6:    �� 
IsReadOnly . 
�������.��6r   5�      �� 5�      -'   5        $��25��5�      $��20��6!   
�������
�������.��5�      �� $$��Resources\icons8-delete-16.png��6   $��14��6@   5�      �� 5�      -(   5        $��search��5�   �   $��Right=�6   $��10,0,0,0q�62   $��25��6D   $	��140��6P   $��15(�6\   6�   �� 5�      $$��Resources\icons8-search-16.png��6   $��16��6<   $��5q�6G   5�      5�      5�      