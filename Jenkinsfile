node {
   stage('Clear Working Directory') { // for display purposes
      sh 'rm -r -f node_modules'
	  sh 'rm -r -f bower_components'
	  sh 'rm -r -f typings'
	  sh 'rm -r -f obj'
	  sh 'rm -r -f bin'
   }
   stage('Copy Files') {
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
            sh 'dotnet restore'
            sh 'dotnet publish'
        }
   }
   stage('Doploy') {
        dir('/opt/p2/netcoreapp1.0') {
            deleteDir()
            sh 'supervisorctl stop PhotoGallery'
        }
        sh 'cp -r src/PhotoGallery/bin/Debug/netcoreapp1.0/* /opt/p2/netcoreapp1.0'
        dir('/opt/p2/netcoreapp1.0') {
            sh 'supervisorctl start PhotoGallery'
        }        
   }   
}