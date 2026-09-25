.class Lcom/helpshift/common/conversation/ConversationDB$ImageAttachmentInfo;
.super Lcom/helpshift/common/conversation/ConversationDB$AttachmentInfo;
.source "ConversationDB.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/common/conversation/ConversationDB;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x2
    name = "ImageAttachmentInfo"
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/common/conversation/ConversationDB;

.field final thumbnailFilePath:Ljava/lang/String;

.field final thumbnailUrl:Ljava/lang/String;


# direct methods
.method constructor <init>(Lcom/helpshift/common/conversation/ConversationDB;Lorg/json/JSONObject;)V
    .locals 1

    .line 1898
    iput-object p1, p0, Lcom/helpshift/common/conversation/ConversationDB$ImageAttachmentInfo;->this$0:Lcom/helpshift/common/conversation/ConversationDB;

    .line 1899
    invoke-direct {p0, p1, p2}, Lcom/helpshift/common/conversation/ConversationDB$AttachmentInfo;-><init>(Lcom/helpshift/common/conversation/ConversationDB;Lorg/json/JSONObject;)V

    const-string p1, "thumbnail_url"

    const/4 v0, 0x0

    .line 1900
    invoke-virtual {p2, p1, v0}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/common/conversation/ConversationDB$ImageAttachmentInfo;->thumbnailUrl:Ljava/lang/String;

    const-string p1, "thumbnailFilePath"

    .line 1901
    invoke-virtual {p2, p1, v0}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/common/conversation/ConversationDB$ImageAttachmentInfo;->thumbnailFilePath:Ljava/lang/String;

    return-void
.end method
