   M S B A M L   `   `   `  ????I  ETableTennisWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:TableTennisWPF.pagesTableTennisWPF.pages  5      ;"clr-namespace:TableTennisWPF.rulesTableTennisWPF.rules  5      )    #TableTennisWPF.pages.CompetitorPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:TableTennisWPF.pages     5      4rules"clr-namespace:TableTennisWPF.rules     5      -   5          N? Title$  CompetitorPage??5
       N? FontSize$ 16(?5      .??5      ? 
??????? ? ColumnDefinitions 5   
   ?? 5      $
??187* ?6   ?? 5      $	??13* ?6   ?? 5      $
??200* ?6    ? RowDefinitions 5   
   ?? 5      $	??250 ?6   ?? 5      ?? 5      $??40 ?6   .+?5   
   ?? -   5         ??Name$ lbCompetitors??5   D   $??2??6[   $??10,10,10,10q?6l   $??2??6?   ??5      ?? 5      .R?5      ??  ?? Orientation$ 
Horizontal=?6!   .+?5      ?? ??6    ?? *Number+?? 5      $
??Bold"?69   ??6    ?? *Name+69   ?? 5       ?? ContentStringFormat$ ({0:dd.MM.yyyy})??6=   ??6    ?? *
BirthDay+6=   5       5!      5#      ?? 5$   
   -   5        $??panelButtons??5$   .   $ 
Horizontal=?6   $??2??6B   $
??Left=?6O   $??Center=?6j   $??2??6?   $??20,0,0,0q?6?   .+?5%      ?? -   5        $	??Új??5%      $	??100??6"   $
??10 0q?6.   6<   ?? 5&      -   5        $??Módosítás??5&      $	??100??6)   $
??10 0q?65   6C   ?? 5'      -   5        $??Törlés??5'      $	??100??6&   $
??10 0q?62   6@   5(      ?? 5)   
   -   5         ??Name$ grpData??5)      $??2??6$   $??10,-2,10,10q?64   $??Adatok szerkeztése??6I   .??5*      ?  5+      ?? 5,      ?? 5-      ?? 5.      ?? 5/      ?? 50       52      ?? 53      $
??auto ?6'   ?? 54      .+?56      ?? $??0??6   $??Név:??6%   $??Center=?64   ?? 57      $??1??6   $??Születési dátum:??6%   $??Center=?6@   ?? 58      $??2??6   $??
Rajtszám:??6%   $??Center=?69   ?? 59      $??3??6   $??Kategória (1-10):??6%   $??Center=?6A   ?? 5;      $??0??6   $??1??6'   $??	10 0 10 0q?67   $??Center=?6J   $??28??6l    ? ToolTip 5<      ?? 	 ?? RelativeSource	 ?? *Self+
 ?? Path$)
 #(Validation.Errors)[0].ErrorContent?.??6}   .??5=      ?? $

 Name?6    ?? UpdateSourceTrigger$ PropertyChanged=?6*    ?? ValidationRules 5>      4   .TableTennisWPF.rules.EmptyStringValidationRule  5?      5A      5B      (  "System.Windows.Controls.DatePicker  5C      -   5        $??
datepicker??5C      $??1??6/   $??1??6<   $??	10 0 10 0q?6L   $??Center=?6_   $??28??6?   $	??124??6?   $
??Left=?6?    5D      ?? 	 ?? *Self+$)
 #(Validation.Errors)[0].ErrorContent?   SelectedDate 5E      ?? 5F      $
 	BirthDate?6"   $ PropertyChanged=?63    5G      2  ,TableTennisWPF.rules.BirthDateValidationRule  5H   "   5J      5L      ?? 5M      $??2??6   $??1??6'   $??	10 0 10 0q?67   $??Center=?6J   $??28??6l   $??60??6x   $
??Left=?6?    5N      ?? 	 ?? *Self+$)
 #(Validation.Errors)[0].ErrorContent?.??6}   .??5O      ?? $
 Number?6   $ PropertyChanged=?6,    5P      /  )TableTennisWPF.rules.NumberValidationRule  5Q      5S      5T      ?? 5U      $??3??6   $??1??6'   $??	10 0 10 0q?67   $??Center=?6J   $??28??6l   $??60??6x   $
??Left=?6?    5V      ?? 	 ?? *Self+$)
 #(Validation.Errors)[0].ErrorContent?.??6}   .??5W      ?? $

 Rank?6   $ PropertyChanged=?6*    5X        5Y      
   Min$ 1??69   
   Max$ 10??6A   5[      5\      ?? 5^      $ 
Horizontal=?6   $??4??66   $??2??6C   $??Center=?6W   $??Center=?6t   .+?5_      ?? -	   5        $??Mentés??5_      $	??100??6.   $??0 0 10 0q?6:   6L   ?? 5`      -
   5        $??Mégse??5`      $	??100??6-   $??10 0 0 0q?69   6K   5a      5b      5c      5d      5e      