while [ -n "$1" ]  
do  
  case "$1" in   
    -build)  
        docker build  -t wasm ./AspDotNet/BlazorWasm/
        ;;  
    -up)  
        docker service update --force  --image wasm:latest  devops_dotnet
        ;;  
    *)  
        echo "$1 is not an option"  
        ;;  
  esac  
done