docker run \
  -v "/var/lib/buildkite/builds:/var/lib/buildkite/builds" \
  -v "/var/run/docker.sock:/var/run/docker.sock" \
  -v "/Users/hailong.yan/.ssh:/root/.ssh" \
  -e "BUILDKITE_BUILD_PATH=/var/lib/buildkite/builds" \
  -d \
  -t \
  --name buildkite-agent \
  buildkite/agent:3 start --token "33144a98cf913f0d502b9d4be5af1cb2c5e607b311c5d42dfd"