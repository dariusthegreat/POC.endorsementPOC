git checkout master
git pull
git branch -d dev
git push origin -d dev
git checkout -b dev
git push --set-upstream origin dev

