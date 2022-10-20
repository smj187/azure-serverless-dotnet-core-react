FROM envoyproxy/envoy-debug-dev:latest

COPY ./envoy.yaml /etc/envoy.yaml
COPY ./configs /var/lib/envoy

ADD ./certificates/server.crt /etc/server.crt
ADD ./certificates/server.key /etc/server.key

RUN chmod go+x /var/lib/envoy && chmod go+r /etc/envoy.yaml /var/lib/envoy/*

CMD ["/usr/local/bin/envoy", "-c /etc/envoy.yaml"]