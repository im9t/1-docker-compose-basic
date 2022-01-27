from email import message
import jieba
from wordSplit_pb2_grpc import WordSplitterServicer
import wordSplit_pb2
import wordSplit_pb2_grpc
import grpc
from concurrent import futures
import time


class myWordSplitServicer(wordSplit_pb2_grpc.WordSplitterServicer):
    def GetSplitted(self, request, context):
        return wordSplit_pb2.KeyWords(keyWords = jieba.cut(request.origin))

    def SayHello(self, request, context):
        return wordSplit_pb2.HelloReply(message = "hello {msg}".format(msg = request.name))
    


def serve():
    # 启动 rpc 服务
    server = grpc.server(futures.ThreadPoolExecutor(max_workers=10))
    wordSplit_pb2_grpc.add_WordSplitterServicer_to_server(myWordSplitServicer(), server)
    server.add_insecure_port('[::]:50051')
    server.start()
    print("server started")
    server.wait_for_termination()
    try:
        while True:
            time.sleep(60*60*24) # one day in seconds
    except KeyboardInterrupt:
        server.stop(0)

if __name__ == '__main__':
    serve()