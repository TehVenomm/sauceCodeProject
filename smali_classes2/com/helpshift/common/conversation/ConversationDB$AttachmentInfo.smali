.class Lcom/helpshift/common/conversation/ConversationDB$AttachmentInfo;
.super Ljava/lang/Object;
.source "ConversationDB.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/common/conversation/ConversationDB;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x2
    name = "AttachmentInfo"
.end annotation


# instance fields
.field final contentType:Ljava/lang/String;

.field final fileName:Ljava/lang/String;

.field final filePath:Ljava/lang/String;

.field final isSecure:Z

.field final size:I

.field final synthetic this$0:Lcom/helpshift/common/conversation/ConversationDB;

.field final url:Ljava/lang/String;


# direct methods
.method constructor <init>(Lcom/helpshift/common/conversation/ConversationDB;Lorg/json/JSONObject;)V
    .locals 2

    .line 1913
    iput-object p1, p0, Lcom/helpshift/common/conversation/ConversationDB$AttachmentInfo;->this$0:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const-string p1, "file_name"

    const/4 v0, 0x0

    .line 1914
    invoke-virtual {p2, p1, v0}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/common/conversation/ConversationDB$AttachmentInfo;->fileName:Ljava/lang/String;

    const-string p1, "content_type"

    .line 1915
    invoke-virtual {p2, p1, v0}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/common/conversation/ConversationDB$AttachmentInfo;->contentType:Ljava/lang/String;

    const-string p1, "url"

    .line 1916
    invoke-virtual {p2, p1, v0}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/common/conversation/ConversationDB$AttachmentInfo;->url:Ljava/lang/String;

    const-string p1, "size"

    const/4 v1, 0x0

    .line 1917
    invoke-virtual {p2, p1, v1}, Lorg/json/JSONObject;->optInt(Ljava/lang/String;I)I

    move-result p1

    iput p1, p0, Lcom/helpshift/common/conversation/ConversationDB$AttachmentInfo;->size:I

    const-string p1, "filePath"

    .line 1918
    invoke-virtual {p2, p1, v0}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/common/conversation/ConversationDB$AttachmentInfo;->filePath:Ljava/lang/String;

    const-string p1, "is_secure"

    .line 1919
    invoke-virtual {p2, p1, v1}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result p1

    iput-boolean p1, p0, Lcom/helpshift/common/conversation/ConversationDB$AttachmentInfo;->isSecure:Z

    return-void
.end method
