# NIPO Software Nfield PowerShell cmdlets

This repo contains a set of PowerShell cmdlets to manage your Nfield services.

# Requirements

* To make use of these cmdlets you need an Nfield account.  
* PowerShell 3.0

# Cmdlets Features

* Domain
  * Open a domain
* Interviewer
  * Get a list of interviewers in your domain
  * Managing interviewers by get, add, update and remove commands (basic CRUD operations)

# Getting Started

## Download Source Code

To get the source code via git just type:

```git clone https://github.com/NIPOSoftware/Nfield-Powershell.git```

## Configure PowerShell to Automatically Load cmdlets

1. Create a folder inside your user's Documents folder and name it __WindowsPowerShell__

2. Inside that folder create a file called __Microsoft.PowerShell_profile.ps1__

3. Edit the file in a text editor and add the following contents

   ```Import-Module PATH_TO_NFIELD-POWERSHELL_CLONE\bin\Release\Manifest\Nfield.PowerShell.psd1```

4. After you build the cmdlets project in Release mode, you can then open a PowerShell window and you should be able to use the cmdlets. Please note that if you want to rebuild the project, you have close the PowerShell window, and then reopen it.

## Manually Loading the cmdlets in a PowerShell Session

1. Build the project under Release mode.

2. Start a new PowerShell session and use the Import-Module cmdlet by passing it the path of the Nfield PowerShell Manifest file __Nfield.PowerShell.psd1__ under the Release output directory

   ```Import-Module PATH_TO_NFIELD-POWERSHELL_CLONE\bin\Release\Manifest\Nfield.PowerShell.psd1```

# Code Samples
Before you can take advantage of the Nfield PowerShell cmdlets you have to sign in to a domain in your PowerShell session

Signing into a domain using your Nfield credentials. This will prompt a username & password dialog and once they're filled in, the information will be stored inside $myCredential variable:

```  $myCredential = get-credential  ```  

Now using the credentials object created with the previous cmdlet this is how to do the actual signing in:

```$myDomain = open-domain -serverurl https://api.nfieldmr.com/v1/ -domainname "testdomain" -credential $myCredential```

After signing in successfully, these are the cmdlets that can be used to manage interviewers:

1. Add interviewer
```
$interviewer = New-Object Nfield.Models.Interviewer -Property @{
  'FirstName'="Sales"; 
  'LastName'="Team"; 
  'Password'="password12"; 
  'ClientInterviewerId'="sales123"; 
  'UserName'="sales"; 
  'EmailAddress'="sales@niposoftware.com"; 
  'TelephoneNumber'="31205225989"
}
add-interviewer -domain $myDomain -input  $interviewer
```

2. Get interviewer 
  1. username equals sales  
``` get-interviewer -domain $myDomain -username sales ```  
  2. clientid equals sales123  
``` get-interviewer -domain $myDomain -clientId sales123 ```  
  3. getting with id  
``` get-interviewer -domain $myDomain -Id INTERVIEWER-ID ```  

3. Update interviewer  
``` $myInterviewer = get-interviewer -domain $myDomain -clientId sales123```   
```  $myInterviewer.FirstName = "Sales Team" ```   
```  set-interviewer -domain $myDomain -input $myInterviewer  ```  

4. Remove interviewer  
``` $myInterviewer = get-interviewer -domain $myDomain -clientId sales123 ```   
``` remove-interviewer -domain $myDomain -input $myInterviewer  ```   

5. Get interviewers  
``` get-interviewers -domain $myDomain ```  

6. Import from csv  
``` import-csv -path FULL_PATH_YOUR_CSV_FILE | add-interviewer -domain $myDomain ```  
Here is an example of a csv file:  
``` 
FirstName,LastName,Password,ClientInterviewerId,UserName,EmailAddress,TelephoneNumber    
Sales,Team,password12,sales123,sales,sales@niposoftware.com,31205225989 
```   


# Feedback
For feedback related to this PowerShell cmdlets project please visit the [Nfield website]( http://www.nfieldmr.com/contact.aspx ) 
