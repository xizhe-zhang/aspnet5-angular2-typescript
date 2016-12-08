node {
   stage('Clear Working Directory') { // for display purposes
        sh 'sudo rm -r -f src/*'
		deleteDir()
   }
   stage('Copy Source Code') { // for display purposes
		sh 'cp -r ../pos-demo-master@script/src .'
   }   
   stage('Copy Build Files') {
        dir('src/PhotoGallery') {
			sh 'cp -r /home/project-pos/* .'
        }
   }   
   stage('Build Gulp') {
        dir('src/PhotoGallery') {
            sh 'gulp build-spa'
        }
   }
   stage('Build dotnet') {
        dir('src/PhotoGallery') {
            sh 'sudo dotnet restore'
            sh 'dotnet publish'
        }
   }
   stage('Doploy') {
        sh 'sudo rm -r -f /opt/p2/netcoreapp1.0/*'
        sh 'sudo supervisorctl stop PhotoGallery'
        sh 'sudo cp -r src/PhotoGallery/bin/Debug/netcoreapp1.0/* /opt/p2/netcoreapp1.0'
        sh 'sudo supervisorctl start PhotoGallery'    
   }   
}