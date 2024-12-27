pipeline {
    agent any

    environment {
        DOCKER_IMAGE = "srinadhnaidu/taskmanagementapi"
        DOCKER_TAG = "latest"
        DOCKER_COMPOSE_FILE = "docker-compose.yml" // Ensure the file exists in your workspace
    }

    stages {
        stage('Checkout Code') {
            steps {
                echo "Checking out source code..."
                checkout scm
            }
        }

        stage('Build Docker Image') {
            steps {
                echo "Building Docker image using Docker Compose..."
                sh """
                    docker compose -f ${DOCKER_COMPOSE_FILE} build
                """
            }
        }
        stage('Push Docker Image') {
            steps {
                echo "Pushing Docker image to Docker Hub..."
                withDockerRegistry([credentialsId: 'docker-hub-credentials', url: 'https://index.docker.io/v1/']) {
                    sh """
                        docker compose -f ${DOCKER_COMPOSE_FILE} push
                    """
                }
            }
        }
    }

    post {
        always {
            echo "Cleaning up Docker Compose resources..."
            cleanWs() // Clean the workspace
        }
    }
}
