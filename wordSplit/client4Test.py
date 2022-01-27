from urllib import response
import grpc
import wordSplit_pb2
import wordSplit_pb2_grpc

def run():
    channel = grpc.insecure_channel('localhost:50051')
    stub = wordSplit_pb2_grpc.WordSplitterStub(channel)
    hellores = stub.SayHello(wordSplit_pb2.HelloRequest(name ="asdfg"))
    print(f"hello returns {hellores.message}")
    response = stub.GetSplitted(wordSplit_pb2.OriginString(origin = "我去上学校天天不迟到"))
    print(response.keyWords)
if __name__ =="__main__":
    print("start")
    run()
    print("end")