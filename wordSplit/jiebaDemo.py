#coding = utf-8
import imp
import jieba
seg_list = jieba.cut("我还是只会,这一点儿技术")
print(",".join(seg_list))