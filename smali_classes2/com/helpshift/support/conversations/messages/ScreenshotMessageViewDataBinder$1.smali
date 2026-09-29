.class Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder$1;
.super Ljava/lang/Object;
.source "ScreenshotMessageViewDataBinder.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder;->bind(Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder;

.field final synthetic val$message:Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;


# direct methods
.method constructor <init>(Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder;Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;)V
    .locals 0

    .line 99
    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder$1;->this$0:Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder;

    iput-object p2, p0, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder$1;->val$message:Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 1

    .line 102
    iget-object p1, p0, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder$1;->this$0:Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder;

    iget-object p1, p1, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder;->messageClickListener:Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;

    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/ScreenshotMessageViewDataBinder$1;->val$message:Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;

    invoke-interface {p1, v0}, Lcom/helpshift/support/conversations/messages/MessageViewDataBinder$MessageItemClickListener;->onScreenshotMessageClicked(Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;)V

    return-void
.end method
