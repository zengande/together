## Create ingress nginx
helm repo add ingress-nginx https://kubernetes.github.io/ingress-nginx
helm install ingress-nginx ingress-nginx/ingress-nginx --namespace ingress-nginx

## EFK
helm upgrade --install es elastic/elasticsearch -f .\k8s\helm\elasticsearch\values.yaml --namespace together
helm upgrade --install fluentd stable/fluentd-elasticsearch -f .\k8s\helm\efk\fluentd.yaml --namespace efk
helm upgrade --install kibana elastic/kibana -f .\k8s\helm\efk\kibana.yaml --namespace efk

## Create TLS secret
kubectl create secret tls www.together2.fun --cert .\1_www.together2.fun_bundle.crt --key .\2_www.together2.fun.key --namespace together

## MySQL
helm upgrade --install mysql stable/mysql -f .\k8s\helm\mysql\values.yaml --namespace together

## RabbitMQ
helm upgrade --install rabbitmq stable/rabbitmq-ha -f ./k8s/helm/rabbitmq/values --namespace together

## User ApiGateway
helm upgrade --install apigatewayuser .\k8s\helm\apigatewayuser\ -f .\k8s\helm\inf.yaml -f .\k8s\helm\ingress-values.yaml --set image.tag=3 --namespace together

## Activity API
helm upgrade --install activityapi .\k8s\helm\activityapi\ -f .\k8s\helm\inf.yaml --set image.tag=39 --namespace together

## Messaging API
helm upgrade --install messagingapi .\k8s\helm\messagingapi\ -f .\k8s\helm\inf.yaml --set image.tag=latest --namespace together

## Web SPA
helm upgrade --install webspa .\k8s\helm\webspa\ -f .\k8s\helm\inf.yaml -f .\k8s\helm\ingress-values.yaml --set image.tag=1 --namespace together

## Test container
kubectl run  -it --rm  cirror-1 --image=cirros -- /bin/sh