# Mała instrukcja
Aby odpalić całą kompozycję dockerową:
 1. Buildujemy aktualną wersję projektu .net `dotnet publish -c Release -o published`
 2. Odpalamy skrypt build-and-run stawiający kompozycję (w zależności od systemu .sh lub .bat) `./build-and-run.sh`
 3. Jak już działa to powinny być otwarte na świat takie porty:
		 `15672` - konsola admina rabbitmq -> dotnetowiec:kocham.Net! (login:haslo)
		 `8081` - gui do mongoDB -> dotnetowiec:lubie.Net! (login:haslo)
		 `8090` - aplikacja .Net 

