# osiris

Trebuie creata o baza de date cu urmatoarele setari:

  host            : 'localhost',
  user            : 'root',
  password        : 'rootpassword',
  database        : 'mysqlcsharp',
  table           : 'mysqlcsharp'
  
  In tabelul mysqlcsharp trebuie sa existe coloanele username, password, admin. Fiecare dintre acestea trebuie sa fie unice fiecarui element inserat, insa chiar daca inseram o parola, aceasta nu va merge intrucat trebuie hash-uita de program. Incepem crearea unui element in care acesta va fi de tipul:
  
  username = numele cu care ne logam
  admin = daca user-ul este admin sau nu
  
  Setam serverul de node.js:
  Editam server.js astfel incat path-uul certificatului si a cheii corespunde cu fisierele downloadate.
  Pentru verificare adaugam rootCA.pem in browser-ul nostru pentru a accesa serverul https://
  Modificari in aplicatie:
  in public Form1() punem in file path-ul catre rootCA.pem
  
  De asemenea deblocam fisierul intrucat visual studio considera fisierul downloadat drept un malicios object de pe internet si deschidem visual studio cu admin privileges.
  
