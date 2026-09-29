.class public interface abstract Lnet/gogame/chat/ChatContext;
.super Ljava/lang/Object;
.source "ChatContext.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/chat/ChatContext$Rating;
    }
.end annotation


# virtual methods
.method public abstract getAgentTypingEntry()Lnet/gogame/chat/AgentTypingEntry;
.end method

.method public abstract getChatEntry(I)Ljava/lang/Object;
.end method

.method public abstract getChatEntryCount()I
.end method

.method public abstract getView(Ljava/lang/Object;ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;
.end method

.method public abstract isAttachmentSupported()Z
.end method

.method public abstract notifyDataSetChanged()V
.end method

.method public abstract registerDataSetObserver(Landroid/database/DataSetObserver;)V
.end method

.method public abstract registerImage(Ljava/lang/String;Landroid/net/Uri;)V
.end method

.method public abstract send(Ljava/io/File;)V
.end method

.method public abstract send(Ljava/lang/String;)V
.end method

.method public abstract send(Lnet/gogame/chat/ChatContext$Rating;)V
.end method

.method public abstract start()V
.end method

.method public abstract stop()V
.end method

.method public abstract unregisterDataSetObserver(Landroid/database/DataSetObserver;)V
.end method
