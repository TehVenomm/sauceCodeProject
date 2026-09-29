.class public interface abstract Lcom/helpshift/support/conversations/NewConversationRouter;
.super Ljava/lang/Object;
.source "NewConversationRouter.java"


# virtual methods
.method public abstract exitNewConversationView()V
.end method

.method public abstract onAuthenticationFailure()V
.end method

.method public abstract showAttachmentPreviewScreenFromDraft(Lcom/helpshift/conversation/dto/ImagePickerFile;)V
.end method

.method public abstract showConversationScreen()V
.end method

.method public abstract showSearchResultFragment(Ljava/util/ArrayList;)V
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/ArrayList<",
            "Lcom/helpshift/support/Faq;",
            ">;)V"
        }
    .end annotation
.end method
