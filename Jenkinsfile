pipeline {

    agent any

    stages {

        stage('Checkout') {
            steps {
                git 'https://github.com/TorresMBA/Mercury.Test.WebApi.git'
            }
        }

        stage('Restore') {
            steps {
                bat 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                bat 'dotnet build'
            }
        }

        stage('Test') {
            steps {
                bat 'dotnet test'
            }
        }
		
		stage('Publish') {
            steps {
                bat 'dotnet publish -c Release -o publish'
            }
        }

        stage('Archive Artifacts') {
            steps {
                archiveArtifacts artifacts: 'publish/**'
            }
        }
		
		stage('Deploy') {
			bat '''
			    %windir%\\System32\\inetsrv\\appcmd stop apppool /apppool.name:"MiApiPool"
			
			    xcopy publish C:\\inetpub\\wwwroot\\Deploy\\Mercury\\ /E /Y /I
			
			    %windir%\\System32\\inetsrv\\appcmd start apppool /apppool.name:"MiApiPool"
			    '''
		}
    }

}
