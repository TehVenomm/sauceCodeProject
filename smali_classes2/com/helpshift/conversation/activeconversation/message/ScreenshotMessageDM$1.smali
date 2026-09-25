.class Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM$1;
.super Ljava/lang/Object;
.source "ScreenshotMessageDM.java"

# interfaces
.implements Lcom/helpshift/downloader/SupportDownloadStateChangeListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->checkAndReDownloadImageIfNotExist(Lcom/helpshift/common/platform/Platform;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;

.field final synthetic val$platform:Lcom/helpshift/common/platform/Platform;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;Lcom/helpshift/common/platform/Platform;)V
    .locals 0

    .line 165
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM$1;->this$0:Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;

    iput-object p2, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM$1;->val$platform:Lcom/helpshift/common/platform/Platform;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onFailure(Ljava/lang/String;)V
    .locals 0

    return-void
.end method

.method public onProgressChange(Ljava/lang/String;I)V
    .locals 0

    return-void
.end method

.method public onSuccess(Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 172
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM$1;->this$0:Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;

    invoke-static {p1, p2}, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->access$003(Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;Ljava/lang/String;)Ljava/lang/String;

    .line 173
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM$1;->val$platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {p1}, Lcom/helpshift/common/platform/Platform;->getConversationDAO()Lcom/helpshift/conversation/dao/ConversationDAO;

    move-result-object p1

    iget-object p2, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM$1;->this$0:Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;

    invoke-interface {p1, p2}, Lcom/helpshift/conversation/dao/ConversationDAO;->insertOrUpdateMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 174
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM$1;->this$0:Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;

    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;->notifyUpdated()V

    return-void
.end method
