helm upgrade --install kong kong/kong -f .\k8s\helm\kong\values.yaml --namespace kong

helm upgrade --install konga .\k8s\helm\konga\ --namespace kong

helm install rabbitmq stable/rabbitmq-ha --set rabbitmqUsername=guest,rabbitmqPassword=VRe5o82jGdA6Em4cSo6cBzaZ,service.type=LoadBalancer,prometheus.operator.enabled=false --namespace together

kubectl run  -it --rm  cirror-1 --image=cirros -- /bin/sh

helm upgrade --install activityapi .\k8s\helm\activityapi\ -f .\k8s\helm\inf.yaml --set image.tag=39 --namespace together

helm upgrade --install messagingapi .\k8s\helm\messagingapi\ -f .\k8s\helm\inf.yaml --set image.tag=latest --namespace together