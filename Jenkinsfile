pipeline {
    agent any

    environment {
        DOCKER_IMAGE = "srinadhnaidu/taskmanagementapi"
        DOCKER_TAG = "latest"
    }

    stages {
        stage('Checkout Code') {
            steps {
                echo "Checking out source code..."
                checkout scm
            }
        }

        stage('Build and Test Docker Compose') {
            steps {
                echo "Building and testing with Docker Compose..."
                sh """
                    docker-compose -f  build
                    docker-compose -f  up --abort-on-container-exit --exit-code-from test
                """
            }
        }

        stage('Push Docker Image') {
            steps {
                echo "Pushing Docker image to Docker Hub..."
                withDockerRegistry([credentialsId: 'docker-hub-credentials', url: 'https://index.docker.io/v1/']) {
                    sh "docker-compose -f  push"
                }
            }
        }
    }

    post {
        always {
            echo "Cleaning up resources..."
            sh "docker-compose -f docker-compose.yml down --volumes --remove-orphans"
            cleanWs()
        }
    }
}
