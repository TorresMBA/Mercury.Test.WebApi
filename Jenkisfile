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

    }

}