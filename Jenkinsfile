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

        stage('Build Docker Image') {
            steps {
                echo "Building Docker image..."
                sh """
                    docker build -t ${DOCKER_IMAGE}:${DOCKER_TAG} .
                """
            }
        }

        stage('Test Docker Image') {
            steps {
                echo "Running tests on Docker image..."
                sh """
                    docker run --rm ${DOCKER_IMAGE}:${DOCKER_TAG} /path/to/test/command
                """
            }
        }

        stage('Push Docker Image') {
            steps {
                echo "Pushing Docker image to Docker Hub..."
                withDockerRegistry([credentialsId: 'docker-hub-credentials', url: 'https://index.docker.io/v1/']) {
                    sh """
                        docker push ${DOCKER_IMAGE}:${DOCKER_TAG}
                    """
                }
            }
        }
    }

    post {
        always {
            echo "Cleaning up resources..."
            sh "docker system prune -f"
            cleanWs()
        }
    }
}
