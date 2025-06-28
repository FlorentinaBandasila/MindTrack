Mind Track

Înainte de a descărca proiectul:
Descărcare IDE: Visual Studio Code: https://code.visualstudio.com/download 
      Visual Studio: https://visualstudio.microsoft.com/downloads/ 
Descărcare SQL Server Express: https://www.microsoft.com/en-gb/sql-server/sql-server-downloads 
Descărcare SSMS: https://learn.microsoft.com/en-us/ssms/install/install 
Instalare Git:  https://git-scm.com/downloads

Pregătire Frontend, în Visual Studio Code:
Clonare proiect GitHub: https://github.com/FlorentinaBandasila/MindTrack_FE.git 
Instalare Flutter SDK: https://docs.flutter.dev/get-started/install
În terminal se va rula: flutter doctor
Instalare dependințe, în terminal: flutter pub get

Pregătire Backend, în Visual Studio:
Clonare proiect GitHub: https://github.com/FlorentinaBandasila/MindTrack.git 
Instalare .NET 8.0 SDK: https://dotnet.microsoft.com/en-us/download
Instalare EF Core (comandă în terminal): dotnet tool install –global dotnet-ef 
Crearea unei baze de date: MindTrack
Modificare „appsettings.json” cu noua conexiune la baza de date
Migrarea bazei de date din Package Manager Console: update-database
Build, în terminal: dotnet build

Pregătire dispozitiv Android fizic: 
Deblocare Developer Options: Settings -> About Phone -> apasă de 7 ori pe Build Number
Se va activa USB Debugging: Developer Options -> selectare USB Debugging
Conectarea la laptop a unui telefon se va face prin cablu USB: Confirmă mesajele de permisiune de pe telefon

Setare regulă Firewall: 
Deschide Windows Defender Firewall -> Advanced Settings
Click Inbound Rules -> New Rule
Rule Type -> Port -> Next
TCP -> Specific local port -> 5175 -> Next
Allow the connection -> Next, selectează toate opțiunile -> nume (Kestrel 5175) -> FInish


Rularea proiectelor:
Deschiderea proiectelor în IDE-urile precizate anterior
Instalarea dependințelor (dacă mai este necesar)
Pornirea aplicației local prin apăsarea butoanelor de RUN
