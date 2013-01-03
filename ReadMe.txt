About the functionality

User enters in the application and can :
- plant new virtual tree and gain real tree planting knowledges (by animation) and leave a green message that encourages others to plant a real trees and save the forest. The application shows how easy is to plant a real tree. 
- view other users trees and their green messages and think about them.


Technologies:

1. WCF Services
2. MVVM light toolkit
3. SQL Server 2008 R2

Build:

1. Generate database from <<PlantATree.sql>> file .
The default settings for the server connection are in the ServiceReferences.ClientConfig file.
- for local test http://localhost/PlantATree/TreeService.svc may be used if database script is generated
- for connection to remote server(which is used for the live app in contest.silverlightshow.net) is:
http://81.161.246.26/PlantATree/TreeService.svc

2. The external libs needed are in the Silverlight app project in Libs folder (libs are  the mvvmlight toolkit v3 bins only)

3. If there are any questions or problems please, let me know at tbmihailov@gmail.com.
