import cv2
import numpy as np
import os
from matplotlib import pyplot as plt

dirname = os.path.dirname(__file__)
filenameimg = dirname + "/Data/arKana.jpg"
filenametmp = dirname + "/Data/testBase.jpg"

img = cv2.imread('D:/t2.jpg')
img_gray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
template = cv2.imread('D:/ixchel.png', 0)
w, h = template.shape[::-1]

res = cv2.matchTemplate(img_gray, template, cv2.TM_CCOEFF_NORMED)
min_val, max_val, min_loc, max_loc = cv2.minMaxLoc(res)


threshold = 0.2
loc = np.where(res >= threshold)


for pt in zip(*loc[::-1]):
    cv2.rectangle(img, pt, (pt[0] + w, pt[1] + h), (0,0,255), 2)

img = np.fliplr(img.reshape(-1,3)).reshape(img.shape)

plt.plot(100), plt.imshow(img, cmap='gray')
plt.title('Detected Point'), plt.xticks([]), plt.yticks([])
plt.suptitle('tresh')

plt.show()
