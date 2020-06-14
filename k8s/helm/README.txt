## Create ingress nginx
helm repo add ingress-nginx https://kubernetes.github.io/ingress-nginx
helm install ingress-nginx ingress-nginx/ingress-nginx --namespace ingress-nginx

## Create TLS secret
kubectl create secret tls www.together2.fun --cert .\1_www.together2.fun_bundle.crt --key .\2_www.together2.fun.key --namespace together

## RabbitMQ
helm install rabbitmq stable/rabbitmq-ha --set rabbitmqUsername=guest,rabbitmqPassword=VRe5o82jGdA6Em4cSo6cBzaZ,service.type=LoadBalancer,prometheus.operator.enabled=false --namespace together

## Activity API
helm upgrade --install activityapi .\k8s\helm\activityapi\ -f .\k8s\helm\inf.yaml --set image.tag=39 --namespace together

## Messaging API
helm upgrade --install messagingapi .\k8s\helm\messagingapi\ -f .\k8s\helm\inf.yaml --set image.tag=latest --namespace together

## Web SPA
helm upgrade --install webspa .\k8s\helm\webspa\ -f .\k8s\helm\inf.yaml -f .\k8s\helm\ingress-values.yaml --set image.tag=1 --namespace together

## Test container
kubectl run  -it --rm  cirror-1 --image=cirros -- /bin/sh